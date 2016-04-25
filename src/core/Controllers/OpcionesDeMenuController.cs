using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Microsoft.AspNet.Mvc;

namespace Bodegas.Controllers
{
    [Route("menu/principal/opciones")]
    public class OpcionesDeMenuController : Controller
    {

        private readonly BodegasContext db;

        public OpcionesDeMenuController(BodegasContext db)
        {
            this.db = db;
        }


        public IActionResult Get()
        {
            var result = new List<object>();
            var opciones = db.OpcionesDeMenu.OrderBy(x => x.Titulo).ToList();

            var padres = opciones.Where(x => x.OpcionPadreId == null).ToList();

            foreach (var padre in padres)
            {
                var hijos = opciones.Where(x => x.OpcionPadreId == padre.Id).ToList();

                result.Add(new
                {
                    padre.Id,
                    padre.Titulo,
                    padre.Ruta,
                    tieneOpciones = hijos.Count > 0,
                    opciones = hijos.Select(x => new
                    {
                        x.Id,
                        x.Titulo,
                        x.Ruta
                    })
                });
            }

            return Ok(result);
        }
    }
}
