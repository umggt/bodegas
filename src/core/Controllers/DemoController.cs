﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class DemoController : Controller
    {
        public IActionResult Get()
        {
            return Ok("Demo");
        }
    }
}
