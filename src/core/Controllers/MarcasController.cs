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


        [HttpGet("{id}", Name = "GetMarca")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await marcas.ObtenerUnicaAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MarcaResumen marca)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            
            var marcaId = await marcas.CrearAsync(marca);
            var result = await marcas.ObtenerUnicaAsync(marcaId);

            return CreatedAtRoute("GetMarca", new { id = marcaId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MarcaResumen marca)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
           
            var modificado = await marcas.EditarAsync(id, marca);

            if (modificado)
            {
                var result = await marcas.ObtenerUnicaAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            var eliminado = await marcas.EliminarMarcaAsync(id);
            if (eliminado)
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
