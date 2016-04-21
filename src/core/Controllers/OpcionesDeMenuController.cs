using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Microsoft.AspNet.Mvc;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class OpcionesDeMenuController : Controller
    {

        private readonly BodegasContext db;

        public OpcionesDeMenuController(BodegasContext db)
        {
            this.db = db;
        }


        public IActionResult Get()
        {
            return Ok(db.OpcionesDeMenu.Select(x => new { x.Id, x.Titulo }).ToList());
        }
    }
}
