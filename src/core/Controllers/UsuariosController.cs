using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Bodegas.Repositorios;
using System.Net;
using Bodegas.Criptografia;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuariosRepositorio usuarios;

        public UsuariosController(UsuariosRepositorio usuarios)
        {
            this.usuarios = usuarios;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await usuarios.ObtenerTodosAsync(new PaginacionParametros(Request));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<IActionResult> GetSingle(int id)
        {            
            id = id == 0 ? User.Id() : id;
            var result = await usuarios.ObtenerUnicoAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDetalle usuario)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var usuarioId = await usuarios.CrearAsync(usuario);
            var result = await usuarios.ObtenerUnicoAsync(usuarioId);
            return CreatedAtRoute("GetUsuario", new { id = usuarioId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioDetalle usuario)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            id = id == 0 ? User.Id() : id;

            var modificado = await usuarios.EditarAsync(id, usuario);
            if (modificado)
            {
                var result = await usuarios.ObtenerUnicoAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.NotModified);
            }
        }

        [HttpPut("perfil/")]
        public async Task<IActionResult> PutContrasenia(Contrasenias claves)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var confirma = await usuarios.ConfirmarClaveActualsync(User.Id(),claves.Actual);

            if (!confirma)
                return Ok("La contraseña actual no es correcta");

            var modificado = await usuarios.CambiarContraseniaAsync(User.Id(), claves);

            if (modificado)
            {                
                return Ok();
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
            }
        }       
    }
}
