using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        [HttpGet("top-10-productos")]
        public IActionResult GetTop10Productos()
        {
            return Ok();
        }
    }
}
