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
    public class RolesController : Controller
    {
        private readonly RolesRepositorio roles;

        public RolesController(RolesRepositorio roles)
        {
            this.roles = roles;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pagina = 1, int elementos = 20)
        {
            var result = await roles.ObtenerTodos(new PaginacionParametros { Pagina = pagina, ElementosPorPagina = elementos });
            return Ok(result);
        }
    }
}
