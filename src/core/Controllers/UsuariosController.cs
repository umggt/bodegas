using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Bodegas.Storage;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuariosStorage usuarios;

        public UsuariosController(UsuariosStorage usuarios)
        {
            this.usuarios = usuarios;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pagina = 1, int elementos = 20)
        {
            var result = await usuarios.ObtenerTodosAsync(new PaginacionParametros { Pagina = pagina, ElementosPorPagina = elementos });
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await usuarios.ObtenerUnicoAsync(id);
            return Ok(result);
        }
    }
}
