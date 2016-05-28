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
    public class MarcasController : Controller
    {
        private readonly MarcasRepositorio marcas;

        public MarcasController(MarcasRepositorio marcas)
        {
            this.marcas = marcas;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await marcas.ObtenerTodasAsync(new PaginacionParametros(Request));
            return Ok(result);
        }
    }
}
