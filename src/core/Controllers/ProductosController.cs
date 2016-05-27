using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Bodegas.Repositorios;
using System.Net;

namespace Bodegas.Controllers
{
    [Route("[controller]")]
    public class ProductosController : Controller
    {
        private readonly ProductosRepositorio productos;

        public ProductosController(ProductosRepositorio productos)
        {
            this.productos = productos;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await productos.ObtenerTodosAsync(new PaginacionParametros(Request));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetProducto")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await productos.ObtenerUnicoAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductoDetalle producto)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var productoId = await productos.CrearAsync(producto);
            var result = await productos.ObtenerUnicoAsync(productoId);
            return CreatedAtRoute("GetProducto", new { id = productoId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDetalle producto)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var modificado = await productos.EditarAsync(id, producto);
            if (modificado)
            {
                var result = await productos.ObtenerUnicoAsync(id);
                return Ok(result);
            }
            else
            {
                return new HttpStatusCodeResult((int) HttpStatusCode.NotModified);
            }
        }
    }
}
