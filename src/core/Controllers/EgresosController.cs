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
    public class EgresosController: Controller
    {
        private readonly EgresosRepositorio egresos;

        public EgresosController(EgresosRepositorio egresos)
        {
            this.egresos = egresos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await egresos.ObtenerTodosAsync(new PaginacionParametros(Request));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetEgreso")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await egresos.ObtenerUnicoAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EgresoDetalle egreso)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            var usuarioId = User.Id();
            var egresoId = await egresos.CrearAsync(egreso, usuarioId);
            var result = await egresos.ObtenerUnicoAsync(egresoId);
            return CreatedAtRoute("GetEgreso", new { id = egresoId }, result);
        }
    }
}
