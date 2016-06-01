using Bodegas.Db;
using Bodegas.Modelos;
using Bodegas.Repositorios;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class TrasladosController : Controller
    {
        private readonly BodegasContext db;
        private readonly EgresosRepositorio egresos;
        private IngresosRepositorio ingresos;

        public TrasladosController(BodegasContext db)
        {
            this.db = db;
            this.egresos = new EgresosRepositorio(db);
            this.ingresos = new IngresosRepositorio(db);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Traslado traslado)
        {
            var usuarioId = User.Id();

            if (traslado.BodegaOrigenId == traslado.BodegaDestinoId)
            {
                throw new InvalidOperationException("La bodega origen y destino no pueden ser la misma.");
            }

            using (var trx = await db.Database.BeginTransactionAsync())
            {
                try
                {
                    traslado.EgresoId = await CrearEgresoAsync(traslado, usuarioId); ;
                    traslado.IngresoId = await CrearIngresoAsync(traslado, usuarioId);
                    trx.Commit();
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
                
            }

            return Ok(traslado);
        }

        private async Task<int> CrearIngresoAsync(Traslado traslado, int usuarioId)
        {
            var productosIds = traslado.Productos.Select(x => x.ProductoId).ToArray();

            var proveedorId = await db.ProveedorProductos.Where(x => productosIds.Contains(x.ProductoId)).Select(x => x.ProveedorId).Distinct().FirstOrDefaultAsync();

            var ingresoId = await ingresos.CrearAsync(usuarioId, new IngresoDetalle
            {
                Fecha = traslado.Fecha,
                BodegaId = traslado.BodegaDestinoId,
                ProveedorId = proveedorId,
                Productos = traslado.Productos.Select(x => new IngresoProductoDetalle
                {
                    Cantidad = x.Cantidad,
                    MarcaId = x.MarcaId,
                    UnidadId = x.UnidadId,
                    ProductoId = x.ProductoId
                }).ToArray()
            });
            return ingresoId;
        }

        private async Task<int> CrearEgresoAsync(Traslado traslado, int usuarioId)
        {
            return await egresos.CrearAsync(usuarioId, new EgresoDetalle
            {
                Fecha = traslado.Fecha,
                BodegaId = traslado.BodegaOrigenId,
                Productos = traslado.Productos
            });
        }
    }
}
