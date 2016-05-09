using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        [HttpGet]
        public IActionResult GetAll() {
            return Ok(new[] { new { Id = 1, Login = "alice", Nombre = "Alice", Correo = "alice@alice.com" } });
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public IActionResult GetSingle(int id)
        {
            return Ok(new { Id = 1, Login = "alice", Nombre = "Alice", Correo = "alice@alice.com" });
        }
    }
}
