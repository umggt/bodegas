using Bodegas.Modelos;
using Bodegas.Repositorios;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class UnidadesDeMedidaController : Controller
    {
        private readonly UnidadesDeMedidaRepositorio unidadesDeMedida;

        public UnidadesDeMedidaController(UnidadesDeMedidaRepositorio unidadesDeMedida)
        {
            this.unidadesDeMedida = unidadesDeMedida;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await unidadesDeMedida.ObtenerTodasAsync(new PaginacionParametros(Request));
            return Ok(result);
        }
    }
}
