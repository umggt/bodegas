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
    public class BodegasController : Controller
    {
        private readonly BodegasRepositorio bodegas;

        public BodegasController(BodegasRepositorio bodegas)
        {
            this.bodegas = bodegas;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await bodegas.ObtenerTodasAsync(new PaginacionParametros(Request));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetBodega")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await bodegas.ObtenerUnicaAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BodegaResumen bodega)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            var usuarioId = User.Id();
            var bodegaId = await bodegas.CrearAsync(bodega, usuarioId);
            var result = await bodegas.ObtenerUnicaAsync(bodegaId);

            return CreatedAtRoute("GetBodega", new { id = bodegaId}, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BodegaResumen bodega)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            var usuarioId = User.Id();
            var modificado = await bodegas.EditarAsync(id, bodega, usuarioId);

            if (modificado)
            {
                var result = await bodegas.ObtenerUnicaAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
            }
           
        }
    }
}
