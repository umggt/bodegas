using Bodegas.Modelos;
using Bodegas.Repositorios;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("productos/{productoId}/unidades")]
    public class ProductosUnidadesController : Controller
    {
        private readonly UnidadesDeMedidaRepositorio unidadesDeMedida;

        public ProductosUnidadesController(UnidadesDeMedidaRepositorio unidadesDeMedida)
        {
            this.unidadesDeMedida = unidadesDeMedida;
        }

        [HttpGet]
        public async Task<IActionResult> GetByProduct(int productoId)
        {
            var result = await unidadesDeMedida.ObtenerPorProductoAsync(productoId, new PaginacionParametros(Request));
            return Ok(result);
        }
    }
}
