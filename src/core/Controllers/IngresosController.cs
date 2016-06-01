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
    public class IngresosController : Controller
    {
        private readonly IngresosRepositorio ingresos;

        public IngresosController(IngresosRepositorio ingresos)
        {
            this.ingresos = ingresos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await ingresos.ObtenerTodosAsync(new PaginacionParametros(Request));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetIngreso")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await ingresos.ObtenerUnicoAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IngresoDetalle modelo)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var id = await ingresos.CrearAsync(User.Id(), modelo);
            var result = await ingresos.ObtenerUnicoAsync(id);

            return CreatedAtRoute("GetIngreso", new { id = result.Id }, result);
        }

    }
}
