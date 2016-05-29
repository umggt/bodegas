using Bodegas.Modelos;
using Bodegas.Repositorios;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class ProveedoresController: Controller
    {
        private readonly ProveedoresRepositorio proveedores;

        public ProveedoresController(ProveedoresRepositorio proveedores)
        {
            this.proveedores = proveedores;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await proveedores.ObtenerTodosAsync(new PaginacionParametros(Request));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetProveedor")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await proveedores.ObtenerUnicoAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProveedorDetalle proveedor)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            var usuarioId = User.Id();
            var proveedorId = await proveedores.CrearAsync(proveedor, usuarioId);
            var result = await proveedores.ObtenerUnicoAsync(proveedorId);
            return CreatedAtRoute("GetProveedor", new { id = proveedorId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProveedorDetalle proveedor)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var usuarioId = User.Id();
            var modificado = await proveedores.EditarAsync(id, proveedor, usuarioId);
            if (modificado)
            {
                var result = await proveedores.ObtenerUnicoAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
            }
        }

        [HttpPost("{id}", Name = "PostTelefonos")]
        public async Task<IActionResult> Post(int id, int[] telefonos)
        {
            bool creado = false;

            if (telefonos.Count() >= 1)
            {
                 creado = await proveedores.CrearTelefonos(id, telefonos);
            }

            if (creado)
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
