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

        [HttpGet("{id}", Name = "GetLista")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await listas.ObtenerUnicaAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ListaDetalle lista)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var listaId = await listas.CrearAsync(lista);
            var result = await listas.ObtenerUnicaAsync(listaId);
            return CreatedAtRoute("GetLista", new { id = listaId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ListaDetalle lista)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var modificado = await listas.EditarAsync(id, lista);
            if (modificado)
            {
                var result = await listas.ObtenerUnicaAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
            }
        }

        [HttpPost ("{idLista}/valores")]
        public async Task<IActionResult> Post(int idLista, [FromBody] ListaValorDetalle valor)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var listaValor = await listas.CrearValorAsync(idLista, valor.Valor);          
            return Created("api/core/listas/" + idLista + "/valores/"+listaValor.Id, listaValor);
        }
    }
}
