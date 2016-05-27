using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Bodegas.Repositorios;
using System.Net;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class ListasController : Controller
    {
        private readonly ListasRepositorio listas;

        public ListasController(ListasRepositorio listas)
        {
            this.listas = listas;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await listas.ObtenerTodasAsync(new PaginacionParametros(Request));
            return Ok(result);
        }

    }
}
