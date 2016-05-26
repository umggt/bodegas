using Bodegas.Modelos;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class BodegasController : Controller
    {
        [HttpGet]
        public IActionResult GetAll(int pagina = 1, int elementos = 20, string ordenamiento = null)
        {
            return Ok(new PaginacionResultado<Object> {
                Pagina = pagina,
                PaginaAnterior = null,
                PaginaSiguiente = null,
                Paginas = new[] { 1 },
                CantidadElementos = 2,
                ElementosPorPagina = elementos,
                TotalPaginas = 1,
                 TotalElementos = 2,
                 Elementos = new[] {
                     new { Id = 1, Nombre = "Bodega Uno"},
                     new { Id = 2, Nombre = "Bodega Dos"}
                 }
            });
        }

        [HttpGet("{id}", Name = "GetBodega")]
        public IActionResult GetSingle(int id)
        {
            return Ok(new { Id = id, Nombre = "Bodega " + id.ToString() });
        }

        [HttpPost]
        public IActionResult Post([FromBody] dynamic modelo)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            return CreatedAtRoute("GetBodega", new { id = 1 }, new { Id = 1, Nombre = "Bodega" });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] dynamic modelo)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            return Ok(modelo);
        }
    }
}
