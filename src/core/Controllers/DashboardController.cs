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
    }
}
