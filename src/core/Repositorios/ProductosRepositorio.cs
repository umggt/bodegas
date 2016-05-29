using System;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;

namespace Bodegas.Repositorios
{
    public class ProductosRepositorio
    {
        private readonly BodegasContext db;

        public ProductosRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<ProductoResumen>> ObtenerTodosAsync(PaginacionParametros paginacion)
        {
            var query = from n in db.Productos
                        select new ProductoResumen
                        {
                            Id = n.Id,
                            Nombre = n.Nombre,
                            Descripcion = n.Descripcion
                        };

            var orderMapping = new OrdenMapping<ProductoResumen> {
                { "id", x => x.Id },
                { "nombre", x => x.Nombre },
                { "descripcion", x=> x.Descripcion }
            };

            var result = await query.OrdenarAsync(paginacion, x => x.Id, orderMapping);

            return result;
        }

        public async Task<ProductoDetalle> ObtenerUnicoAsync(int id)
        {
            var producto = await db.Productos.Include(x => x.Caracteristicas).Include(x => x.Marcas).Include(x => x.UnidadesDeMedida).SingleOrDefaultAsync(x => x.Id == id);

            if (producto == null)
            {
                throw new RegistroNoEncontradoException($"No existe ningún producto con el id {id}.");
            }



            return new ProductoDetalle
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Marcas = producto.Marcas.Select(x => x.MarcaId).ToArray(),
                Unidades = producto.UnidadesDeMedida.Select(x => x.UnidadDeMedidaId).ToArray(),
                Caracteristicas = producto.Caracteristicas.Select(x => new CaracteristicaDetalle {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Tipo = (int) x.TipoCaracteristica,
                    TipoNombre = x.TipoCaracteristica.ToString().SplitByUpperCase(),
                    Requerido = x.Requerido,
                    ListaId = x.ListaId,
                    Minimo = x.Minimo,
                    Maximo = x.Maximo
                }).ToArray()
            };
        }

        public async Task<int> CrearAsync(ProductoDetalle producto)
        {
            var nombre = producto.Nombre.Trim();
            var descripcion = producto.Descripcion.Trim();

            if (await ExisteNombre(nombre))
            {
                throw new InvalidOperationException($"Ya existe un producto con el nombre '{nombre}'.");
            }

            var nuevoProducto = new Producto {
                Nombre = nombre,
                Descripcion = descripcion
            };

            if (producto.Marcas != null && producto.Marcas.Length > 0)
            {

            }

            db.Productos.Add(nuevoProducto);

            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevoProducto.Id;
            }
            else
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(int id, ProductoDetalle producto)
        {
            var productoAEditar = await db.Productos.SingleOrDefaultAsync(x => x.Id == id);

            if (productoAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe el producto {id}");
            }

            var nombre = producto.Nombre.Trim();
            if (await ExisteNombreEnOtroProducto(id, nombre))
            {
                throw new InvalidOperationException($"Ya existe otro producto con el nombre '{nombre}'.");
            }

            productoAEditar.Nombre = nombre;
            productoAEditar.Descripcion = producto.Descripcion.Trim();
                 
            var filasAfectadas = await db.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var productoAEliminar = await db.Productos.SingleOrDefaultAsync(x => x.Id == id);

            if (productoAEliminar == null)
            {
                throw new RegistroNoEncontradoException($"No existe el producto {id}");
            }

            db.Productos.Remove(productoAEliminar);

            var filasAfectadas = await db.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        private Task<bool> ExisteNombre(string nombre)
        {
            return db.Productos.AnyAsync(x => x.Nombre.ToLower() == nombre.ToLower());
        }

        private Task<bool> ExisteNombreEnOtroProducto(int productoAIgnorar, string nombre)
        {
            return db.Productos.AnyAsync(x => x.Id != productoAIgnorar && x.Nombre.ToLower() == nombre.ToLower());
        }

        
    }
}
