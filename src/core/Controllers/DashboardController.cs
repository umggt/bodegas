using Bodegas.Db;
using Bodegas.Modelos;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly BodegasContext db;

        public DashboardController(BodegasContext db)
        {
            this.db = db;
        }

        [HttpGet("top-10-productos")]
        public IActionResult GetTop10Productos()
        {
            return Ok();
        }

        [HttpGet("resumen")]
        public async Task<IActionResult> GetResumen()
        {

            var proveedoresCount = db.Proveedores.CountAsync();
            var bodegasCount = db.Bodegas.CountAsync();
            var productosCount = db.Productos.CountAsync();
            var marcasCount = db.Marcas.CountAsync();

            var result = new DashboardResumen
            {
                Proveedores = await proveedoresCount,
                Bodegas = await bodegasCount,
                Productos = await productosCount,
                Marcas = await marcasCount
            };

            return Ok(result);
        }

        [HttpGet("ingresos-vs-egresos")]
        public async Task<IActionResult> GetIngresosVsEgresos()
        {

            // TODO: arreglar este query, ahorita se realizó así porque la versión de Entity Framework que está
            //       ejecutándose actualmente tiene algunos issues con los unions, groups, etc. por lo que hay
            //       que realizar pruebas actualizando la dependencia de EF.

            var ingresosTask = db.IngresoProductos.Select(x => new { x.Id, x.Ingreso.Fecha, Ingresos = x.Cantidad, Egresos = 0m }).ToArrayAsync();
            var egresosTask = db.EgresoProductos.Select(x => new { x.Id, x.Egreso.Fecha, Ingresos = 0m, Egresos = x.Cantidad }).ToArrayAsync();

            var ingresos = await ingresosTask;
            var egresos = await egresosTask;
            var todos = ingresos.Union(egresos).GroupBy(x => x.Fecha).Select(x => new { Fecha = x.Key, Ingresos = x.Sum(y => y.Ingresos), Egresos = x.Sum(y => y.Egresos) });

            var result = todos.ToArray();

            return Ok(result);
        }
    }
}
