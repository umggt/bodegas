using Bodegas.Repositorios;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Controllers
{
    [Route("productos/{productoId}/caracteristicas")]
    public class ProductosCaracteristicasController : Controller
    {
        private readonly CaracteristicasRepositorio caracteristicas;

        public ProductosCaracteristicasController(CaracteristicasRepositorio caracteristicas)
        {
            this.caracteristicas = caracteristicas;
        }

        public async Task<IActionResult> GetByProduct(int productoId)
        {
            var result = await caracteristicas.ObtenerPorProducto(productoId);
            return Ok(result);
        }
    }
}
