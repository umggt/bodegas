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

        [HttpGet("{id}", Name = "GetUnidadMedida")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await unidadesDeMedida.ObtenerUnicaAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UnidadDeMedidaResumen unidadMedida)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var unidadMedidaId = await unidadesDeMedida.CrearAsync(unidadMedida);
            var result = await unidadesDeMedida.ObtenerUnicaAsync(unidadMedidaId);

            return CreatedAtRoute("GetUnidadMedida", new { id = unidadMedidaId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UnidadDeMedidaResumen unidadMedida)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var modificado = await unidadesDeMedida.EditarAsync(id, unidadMedida);

            if (modificado)
            {
                var result = await unidadesDeMedida.ObtenerUnicaAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
            }

        }
    }
}
