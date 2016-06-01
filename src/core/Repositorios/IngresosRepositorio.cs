using Bodegas.Db;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Bodegas.Db.Entities.ProductoTipoCaracteristica;

namespace Bodegas.Repositorios
{
    public class IngresosRepositorio
    {
        private readonly BodegasContext db;

        public IngresosRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<IngresoResumen>> ObtenerTodosAsync(PaginacionParametros paginacion)
        {
            var query = from i in db.Ingresos
                        select new IngresoResumen
                        {
                            Id = i.Id,
                            Bodega = i.Bodega.Nombre,
                            Proveedor = i.Proveedor.Nombre,
                            Fecha = i.Fecha
                        };

            var mapping = new OrdenMapping<IngresoResumen>
            {
                { "id", x => x.Id },
                { "bodega", x => x.Bodega },
                { "proveedor", x => x.Proveedor },
                { "fecha", x => x.Fecha }
            };

            var ingresos = await query.OrdenarAsync(paginacion, x => x.Fecha, mapping, true);

            if (ingresos.CantidadElementos > 0)
            {
                var ingresosIds = ingresos.Elementos.Select(x => x.Id).ToArray();

                var productosQuery = from p in db.IngresoProductos
                                     where ingresosIds.Contains(p.IngresoId)
                                     select new IngresoProductoResumen
                                     {
                                         Id = p.Id,
                                         IngresoId = p.IngresoId,
                                         Producto = p.Producto.Nombre,
                                         Marca = p.Marca.Nombre,
                                         Unidad = p.UnidadDeMedida.Nombre,
                                         Cantidad = p.Cantidad,
                                         Precio = p.Precio
                                     };

                var productos = await productosQuery.ToArrayAsync();

                foreach (var ingreso in ingresos.Elementos)
                {
                    ingreso.Productos = productos.Where(x => x.IngresoId == ingreso.Id).ToArray();
                }
            }

            return ingresos;
        }

        public async Task<IngresoDetalle> ObtenerUnicoAsync(int id)
        {
            var ingreso = await db.Ingresos
                .Include(x => x.Bodega)
                .Include(x => x.Proveedor)
                .Include(x => x.Productos)
                    .ThenInclude(x => x.Marca)
                .Include(x => x.Productos)
                    .ThenInclude(x => x.UnidadDeMedida)
                .Include(x => x.Productos)
                    .ThenInclude(x => x.Producto)
                .Include(x => x.Productos)
                    .ThenInclude(x => x.Caracteristicas)
                        .ThenInclude(x => x.Caracteristica)
                .Include(x => x.Productos)
                    .ThenInclude(x => x.Caracteristicas)
                        .ThenInclude(x => x.ListaValor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ingreso == null)
            {
                throw new RegistroNoEncontradoException($"No existe ningún ingreso con id {id}");
            }

            var result = new IngresoDetalle {
                Id = ingreso.Id,
                Fecha = ingreso.Fecha,
                BodegaId = ingreso.BodegaId,
                BodegaNombre = ingreso.Bodega.Nombre,
                ProveedorId = ingreso.ProveedorId,
                ProveedorNombre = ingreso.Proveedor.Nombre,
                Productos = ingreso.Productos.Select(producto => new IngresoProductoDetalle {
                    Id = producto.Id,
                    IngresoId = producto.IngresoId,
                    ProductoId = producto.ProductoId,
                    Nombre = producto.Producto.Nombre,
                    MarcaId = producto.MarcaId,
                    MarcaNombre = producto.Marca.Nombre,
                    UnidadId = producto.UnidadDeMedidaId,
                    UnidadNombre = producto.UnidadDeMedida.Nombre,
                    Cantidad = producto.Cantidad,
                    Precio = producto.Precio,
                    Serie = producto.NumeroDeSerie,
                    Caracteristicas = producto.Caracteristicas.Select(caracteristica => new IngresoProductoCaracteristicaDetalle {
                        Id = caracteristica.Id,
                        IngresoProductoId = caracteristica.IngresoProductoId,
                        Nombre = caracteristica.Caracteristica.Nombre,
                        Valor = caracteristica.Valor,
                        ListaValorId = caracteristica.ListaValorId,
                        ListaValor = caracteristica.ListaValor?.Valor,
                        EsLista = EsLista(caracteristica),
                        EsNumero = EsNumero(caracteristica),
                        EsBooleano = EsBooleano(caracteristica),
                        EsMoneda = EsMoneda(caracteristica),
                        EsTexto = EsTexto(caracteristica)
                    }).ToArray()
                }).ToArray()
            };

            return result;
        }

        private bool EsTexto(IngresoProductoCaracteristica caracteristica)
        {
            var tipo = caracteristica.Caracteristica.TipoCaracteristica;
            return tipo == TextoLargo || tipo == TextoCorto;
        }

        private bool EsMoneda(IngresoProductoCaracteristica caracteristica)
        {
            var tipo = caracteristica.Caracteristica.TipoCaracteristica;
            return tipo == Moneda;
        }

        private bool EsBooleano(IngresoProductoCaracteristica caracteristica)
        {
            var tipo = caracteristica.Caracteristica.TipoCaracteristica;
            return tipo == Booleano;
        }

        private bool EsNumero(IngresoProductoCaracteristica caracteristica)
        {
            var tipo = caracteristica.Caracteristica.TipoCaracteristica;
            return tipo == NumeroEnteroPositivo || tipo == NumeroDecimalPositivo || tipo == NumeroEntero || tipo == NumeroDecimal;
        }

        private bool EsLista(IngresoProductoCaracteristica caracteristica)
        {
            var tipo = caracteristica.Caracteristica.TipoCaracteristica;
            return tipo == SeleccionSimple || tipo == SeleccionMultiple;
        }

        public async Task<int> CrearAsync(int usuarioId, IngresoDetalle ingreso)
        {
            if (ingreso == null)
            {
                throw new ArgumentNullException(nameof(ingreso));
            }

            if (ingreso.Productos == null || ingreso.Productos.Length == 0)
            {
                throw new InvalidOperationException("No puede crear un ingreso sin productos.");
            }

            var tieneProductosRepetidos = ingreso.Productos.GroupBy(x => new { x.ProductoId, x.MarcaId, x.UnidadId }).Where(x => x.Count() > 1).Any();

            if (tieneProductosRepetidos)
            {
                throw new InvalidOperationException("Un ingreso no puede tener un producto repetido con la misma marca y unidad de medida (debe crear dos ingresos distintos)");
            }

            var hayProductosSinCantidad = ingreso.Productos.Any(x => x.Cantidad <= 0);

            if (hayProductosSinCantidad)
            {
                throw new InvalidOperationException("Un ingreso no puede contener productos con valores menores o iguales a cero");
            }

            var hayProductosConPrecioNegativo = ingreso.Productos.Any(x => x.Precio < 0);

            if (hayProductosConPrecioNegativo)
            {
                throw new InvalidOperationException("Un ingreso no puede contener productos con precio negativo");
            }

            var existeProveedor = await db.Proveedores.AnyAsync(x => x.Id == ingreso.ProveedorId);
            if (!existeProveedor)
            {
                throw new InvalidOperationException($"No existe el proveedor con id {ingreso.ProveedorId}");
            }

            var existeBodega = await db.Bodegas.AnyAsync(x => x.Id == ingreso.BodegaId);
            if (!existeBodega)
            {
                throw new InvalidOperationException($"No existe una bodega con id {ingreso.BodegaId}");
            }

            var productosIds = ingreso.Productos.Select(x => x.ProductoId).Distinct().ToArray();
            var cantidadDeProductos= await db.Productos.CountAsync(x => productosIds.Contains(x.Id));
            var existenTodosLosProductos = cantidadDeProductos == productosIds.Length;
            if (!existenTodosLosProductos)
            {
                throw new InvalidOperationException("Al menos uno de los productos indicados no existe");
            }

            var marcasParaLosProductosTask = db.ProductoMarcas.Where(x => productosIds.Contains(x.ProductoId)).ToArrayAsync();
            var unidadesParaLosProductosTask = db.ProductoUnidadesDeMedida.Where(x => productosIds.Contains(x.ProductoId)).ToArrayAsync();
            //var caracteristicasParaLosProductosTask = db.ProductoCaracteristicas.Where(x => productosIds.Contains(x.ProductoId)).ToArrayAsync();

            var marcasParaLosProductos = await marcasParaLosProductosTask;
            var unidadesParaLosProductos = await unidadesParaLosProductosTask;
            //var caracteristicasParaLosProductos = await caracteristicasParaLosProductosTask;

            foreach (var item in ingreso.Productos)
            {
                if (!marcasParaLosProductos.Any(x => x.MarcaId == item.MarcaId && x.ProductoId == item.ProductoId))
                {
                    throw new InvalidOperationException($"No existe una marca con id {item.MarcaId} o no está asignada al producto con id {item.ProductoId}");
                }
                if (!unidadesParaLosProductos.Any(x => x.UnidadDeMedidaId == item.UnidadId && x.ProductoId == item.ProductoId))
                {
                    throw new InvalidOperationException($"No existe una unidad de medida con id {item.UnidadId} o no está asignada al producto con id {item.ProductoId}");
                }

                // TODO: Validar caracteristicas (requeridas, existentes, etc.)
                
            }


            // NOTA: muchas de las validaciones anteriores podrían implementarse como DataAnnotations
            //       para que puedan ser utilizadas por otros controllers y delegar esas tareas de 
            //       validación al motor de MVC por medio de atributos en los modelos.

            if (ingreso.Fecha == default(DateTime))
            {
                ingreso.Fecha = DateTime.UtcNow;
            }

            var nuevoIngreso = new Ingreso {
                Fecha = ingreso.Fecha.ToUniversalTime(),
                UsuarioId = usuarioId,
                BodegaId = ingreso.BodegaId,
                ProveedorId = ingreso.ProveedorId,
                Productos = ingreso.Productos.Select(producto => new IngresoProducto {
                    ProductoId = producto.ProductoId,
                    MarcaId = producto.MarcaId,
                    UnidadDeMedidaId = producto.UnidadId,
                    Cantidad = producto.Cantidad,
                    Precio = producto.Precio,
                    NumeroDeSerie = producto.Serie,
                    Caracteristicas = producto.Caracteristicas.Select(caracteristica => new IngresoProductoCaracteristica {
                        CaracteristicaId = caracteristica.Id,
                        ListaValorId = caracteristica.ListaValorId,
                        Valor = caracteristica.Valor
                    }).ToArray()
                }).ToArray()
            };

            db.Ingresos.Add(nuevoIngreso);

            var existencias = await db.Existencias.Include(x => x.Cantidades).Where(x => productosIds.Contains(x.ProductoId)).ToListAsync();

            foreach (var item in ingreso.Productos)
            {
                var productoId = item.ProductoId;
                var nuevaCantidad = item.Cantidad;
                var marcaId = item.MarcaId;
                var unidadId = item.UnidadId;
                var existencia = existencias.FirstOrDefault(x => x.ProductoId == productoId);
                if (existencia == null)
                {
                    existencia = new Existencia
                    {
                        ProductoId = productoId,
                        Cantidades = new[] {
                            new ExistenciaCantidad {
                                Cantidad = nuevaCantidad,
                                FechaModificacion = DateTime.UtcNow,
                                MarcaId = marcaId,
                                UnidadDeMedidaId = unidadId
                            }
                        }
                    };

                    db.Existencias.Add(existencia);
                    existencias.Add(existencia);
                }
                else
                {
                    var cantidad = existencia.Cantidades.FirstOrDefault(x => x.UnidadDeMedidaId == unidadId && x.MarcaId == marcaId);
                    if (cantidad == null)
                    {
                        cantidad = new ExistenciaCantidad {
                            Cantidad = nuevaCantidad,
                            FechaModificacion = DateTime.UtcNow,
                            MarcaId = marcaId,
                            UnidadDeMedidaId = unidadId
                        };
                        existencia.Cantidades.Add(cantidad);
                    }
                    else
                    {
                        cantidad.Cantidad += nuevaCantidad;
                    }
                }
            }

            await db.SaveChangesAsync();

            return nuevoIngreso.Id;
        }

        
    }
}
