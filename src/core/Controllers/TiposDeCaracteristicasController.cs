using Bodegas.Db.Entities;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class TiposDeCaracteristicasController: Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var tipos = Enum.GetValues(typeof(ProductoTipoCaracteristica));
            var result = new List<object>(tipos.Length);

            var regex = new Regex("[A-Z]");

            foreach (var item in tipos)
            {
                result.Add(new
                {
                    Id = (int)item,
                    Nombre = item.ToString().SplitByUpperCase()
                });
            }

            return Ok(result);
        }
    }
}
