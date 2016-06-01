using Bodegas.Db;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Repositorios
{
    public class EgresosRepositorio
    {
        private readonly BodegasContext db;

        public EgresosRepositorio(BodegasContext db)
        {
            this.db = db;
        }
        public async Task<PaginacionResultado<EgresoResumen>> ObtenerTodosAsync(PaginacionParametros paginacion)
        {
            var query = from e in db.Egresos
                        select new EgresoResumen
                        {
                            Id = e.Id,
                            Bodega = e.Bodega.Nombre,
                            Fecha = e.Fecha
                        };

            var orderMapping = new OrdenMapping<EgresoResumen> {
                { "id", x => x.Id },
                { "Bodega", x => x.Bodega },
                { "Fecha", x=> x.Fecha }
            };

            var result = await query.OrdenarAsync(paginacion, x => x.Id, orderMapping);

            return result;
        }

        public async Task<EgresoDetalle> ObtenerUnicoAsync(int id)
        {
            var egreso = await db.Egresos.Include(x => x.Bodega).Include(x => x.Productos).ThenInclude(x => x.Marca).Include(x => x.Productos).ThenInclude(x => x.UnidadDeMedida).Include(x => x.Productos).ThenInclude(x => x.Producto).Include(x => x.Usuario).SingleOrDefaultAsync(x => x.Id == id);

            if (egreso == null)
            {
                throw new RegistroNoEncontradoException($"No existe ningún egreso con el id {id}.");
            }

            return new EgresoDetalle
            {
                EgresoId = egreso.Id,
                BodegaId = egreso.BodegaId,
                Fecha = egreso.Fecha,
                Productos = egreso.Productos.Select(x => new ProductoEgresoDetalle
                {
                    EgresoId = x.Id,
                    ProductoId = x.ProductoId,
                    UnidadDeMedida = x.UnidadDeMedida.Nombre,
                    UnidadId = x.UnidadDeMedidaId,
                    Cantidad = x.Cantidad
                }).ToArray()
            };
        }

        public async Task<int> CrearAsync(int usuarioId, EgresoDetalle egreso)
        {
            if (egreso == null)
            {
                throw new ArgumentNullException(nameof(egreso));
            }

            if (egreso.Productos == null || egreso.Productos.Count() == 0)
            {
                throw new InvalidOperationException("No puede crear un egreso sin productos.");
            }
            var tieneProductosRepetidos = egreso.Productos.GroupBy(x => new { x.ProductoId, x.MarcaId, x.UnidadId }).Where(x => x.Count() > 1).Any();

            if (tieneProductosRepetidos)
            {
                throw new InvalidOperationException("Un egreso no puede tener un producto repetido con la misma marca y unidad de medida (debe crear dos egresos distintos)");
            }

            var hayProductosSinCantidad = egreso.Productos.Any(x => x.Cantidad <= 0);

            if (hayProductosSinCantidad)
            {
                throw new InvalidOperationException("Un egreso no puede contener productos con valores menores o iguales a cero");
            }

            var existeBodega = await db.Bodegas.AnyAsync(x => x.Id == egreso.BodegaId);
            if (!existeBodega)
            {
                throw new InvalidOperationException($"No existe una bodega con id {egreso.BodegaId}");
            }

            var productosIds = egreso.Productos.Select(x => x.ProductoId).Distinct().ToArray();
            var cantidadDeProductos = await db.Productos.CountAsync(x => productosIds.Contains(x.Id));
            var existenTodosLosProductos = cantidadDeProductos == productosIds.Length;
            if (!existenTodosLosProductos)
            {
                throw new InvalidOperationException("Al menos uno de los productos indicados no existe");
            }

            var marcasParaLosProductos = await db.ProductoMarcas.Where(x => productosIds.Contains(x.ProductoId)).ToArrayAsync();
            var unidadesParaLosProductos = await db.ProductoUnidadesDeMedida.Where(x => productosIds.Contains(x.ProductoId)).ToArrayAsync();
            
            foreach (var item in egreso.Productos)
            {
                if (!marcasParaLosProductos.Any(x => x.MarcaId == item.MarcaId && x.ProductoId == item.ProductoId))
                {
                    throw new InvalidOperationException($"No existe una marca con id {item.MarcaId} o no está asignada al producto con id {item.ProductoId}");
                }
                if (!unidadesParaLosProductos.Any(x => x.UnidadDeMedidaId == item.UnidadId && x.ProductoId == item.ProductoId))
                {
                    throw new InvalidOperationException($"No existe una unidad de medida con id {item.UnidadId} o no está asignada al producto con id {item.ProductoId}");
                }

            }

            if (egreso.Fecha == default(DateTime))
            {
                egreso.Fecha = DateTime.UtcNow;
            }

            var nuevoEgreso = new Egreso
            {
                Fecha = egreso.Fecha.ToUniversalTime(),
                UsuarioId = usuarioId,
                BodegaId = egreso.BodegaId
            };

            if (egreso.Productos != null && egreso.Productos.Count() > 0)
            {
                nuevoEgreso.Productos = egreso.Productos.Select(producto => new EgresoProducto {
                    Egreso = nuevoEgreso,
                    ProductoId = producto.ProductoId,
                    UnidadDeMedidaId = producto.UnidadId,
                    Cantidad = producto.Cantidad,
                    MarcaId = producto.MarcaId
                }).ToList();
            }

            db.Egresos.Add(nuevoEgreso);

            var existencias = await db.Existencias.Include(x => x.Cantidades).Where(x => productosIds.Contains(x.ProductoId)).ToListAsync();
            foreach (var item in egreso.Productos)
            {
                var productoId = item.ProductoId;
                var nuevaCantidad = item.Cantidad;
                var marcaId = item.MarcaId;
                var unidadId = item.UnidadId;
                var existencia = existencias.FirstOrDefault(x => x.ProductoId == productoId);
                if (existencia == null)
                {
                    throw new InvalidOperationException($"No hay existencias suficientes para el producto {item.ProductoId}");
                }
                else
                {
                    var cantidad = existencia.Cantidades.FirstOrDefault(x => x.UnidadDeMedidaId == unidadId && x.MarcaId == marcaId);
                    if (cantidad == null)
                    {
                        throw new InvalidOperationException($"No hay existencias suficientes para el producto {item.ProductoId}");
                    }
                    else
                    {
                        if (cantidad.Cantidad < nuevaCantidad)
                        {
                            throw new InvalidOperationException($"La cantidad que se desea sacar del producto {item.ProductoId} es mayor a la cantidad disponible");
                        }
                        else
                        {
                            cantidad.Cantidad -= nuevaCantidad;
                        }
                    }

                }
            }

            await db.SaveChangesAsync();

            return nuevoEgreso.Id;

        }


    }
}
