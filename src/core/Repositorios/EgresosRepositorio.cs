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
            var egreso = await db.Egresos.Include(x => x.Bodega).Include(x => x.Productos).Include(x => x.Usuario).SingleOrDefaultAsync(x => x.Id == id);

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
                    UnidadDeMedidaId = x.UnidadDeMedidaId,
                    cantidad = x.Cantidad
                }).ToArray()
            };
        }

        public async Task<int> CrearAsync(EgresoDetalle egreso, int usuarioId)
        {
           
        
            var nuevoEgreso = new Egreso
            {
                BodegaId = egreso.BodegaId,
                Fecha = egreso.Fecha,
                UsuarioId = usuarioId
            };

            if (egreso.Productos != null && egreso.Productos.Count() > 0)
            {
                nuevoEgreso.Productos = egreso.Productos.Select(productoId => new EgresoProducto
                {
                    Egreso = nuevoEgreso,
                    ProductoId = productoId.ProductoId,
                    UnidadDeMedidaId = productoId.UnidadDeMedidaId,
                    Cantidad = productoId.cantidad
                }).ToList();
            }

          
            db.Egresos.Add(nuevoEgreso);

            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevoEgreso.Id;
            }
            else
            {
                return -1;
            }
        }


    }
}
