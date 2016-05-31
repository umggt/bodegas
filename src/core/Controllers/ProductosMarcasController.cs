using Bodegas.Modelos;
using Bodegas.Repositorios;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("productos/{productoId}/marcas")]
    public class ProductosMarcasController : Controller
    {
        private readonly MarcasRepositorio marcas;

        public ProductosMarcasController(MarcasRepositorio marcas)
        {
            this.marcas = marcas;
        }

        [HttpGet]
        public async Task<IActionResult> GetByProduct(int productoId)
        {
            var result = await marcas.ObtenerPorProductoAsync(productoId, new PaginacionParametros(Request));
            return Ok(result);
        }
    }
}
