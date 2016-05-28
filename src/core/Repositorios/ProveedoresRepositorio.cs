using Bodegas.Db;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Repositorios
{
    public class ProveedoresRepositorio
    {
        private readonly BodegasContext db;
        public ProveedoresRepositorio(BodegasContext db)
        {
            this.db = db;
        }


        public async Task<PaginacionResultado<ProveedorResumen>> ObtenerTodosAsync(PaginacionParametros paginacion)
        {
            var query = from p in db.Proveedores
                        select new ProveedorResumen
                        {
                            Id = p.Id,
                            Nombre = p.Nombre,
                            NombreContacto = p.NombreDeContacto
                        };

            var orderMapping = new OrdenMapping<ProveedorResumen> {
                { "id", x => x.Id },
                { "nombre", x => x.Nombre },
                { "nombreContacto", x => x.NombreContacto }
            };

            var result = await query.OrdenarAsync(paginacion, x => x.Id, orderMapping);

            return result;
        }

        public async Task<ProveedorDetalle> ObtenerUnicoAsync(int id)
        {
            var proveedor = await db.Proveedores.Include(x => x.Telefonos).Include(x => x.Productos).SingleAsync(x => x.Id == id);

            return new ProveedorDetalle
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                NombreDeContacto = proveedor.NombreDeContacto,
                Direccion = proveedor.Direccion
            };
        }

        public async Task<int> CrearAsync(ProveedorDetalle proveedor, int usuarioId)
        {
            var nombre = proveedor.Nombre.Trim().ToLowerInvariant();
            var nombreContacto = proveedor.NombreDeContacto.Trim().ToLowerInvariant();
            var direccion = proveedor.Direccion.Trim().ToLowerInvariant();
           
            if (await ExisteProveedor(nombre))
            {
                throw new InvalidOperationException($"El proveedor {nombre} ya existe.");
            }

            var nuevoProveedor = new Proveedor
            {
                Nombre = nombre,
                NombreDeContacto = nombreContacto,
                Direccion = direccion,
                UsuarioCreacionId = usuarioId,
                UsuarioModificaId = usuarioId
            };

            db.Proveedores.Add(nuevoProveedor);

            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevoProveedor.Id;
            }
            else
            {
                return -1;
            }
        }


        private Task<bool> ExisteProveedor(string nombre)
        {
            return db.Proveedores.AnyAsync(x => x.Nombre.ToLower() == nombre);
        }

        public async Task<bool> EditarAsync(int id, ProveedorDetalle proveedor, int usuarioId)
        {
            var proveedorAEditar = await db.Proveedores.Include(x => x.Telefonos).Include(x => x.Productos).SingleOrDefaultAsync(x => x.Id == id);

            if (proveedorAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe el proveedor {id}");
            }

            var nombre = proveedor.Nombre.Trim().ToLowerInvariant();
            var nombreContacto = proveedor.NombreDeContacto.Trim().ToLowerInvariant();
            var direccion = proveedor.Direccion.Trim().ToLowerInvariant();

            proveedorAEditar.Nombre = nombre;
            proveedorAEditar.NombreDeContacto = nombreContacto;
            proveedorAEditar.Direccion = direccion;
            proveedorAEditar.UsuarioCreacionId = usuarioId;
            proveedorAEditar.UsuarioModificaId = usuarioId;

            var filasAfectadas = await db.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> CrearTelefonos(int id, int[] telefonos)
        {
            var proveedorAEditar = await db.Proveedores.Include(x => x.Telefonos).Include(x => x.Productos).SingleOrDefaultAsync(x => x.Id == id);

            if (proveedorAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe el proveedor {id}");
            }

            foreach (var item in proveedorAEditar.Telefonos)
            {
                proveedorAEditar.Telefonos.Remove(item);
            }

            foreach (var item in telefonos)
            {
                
                var nuevoTelefonoProveedor = new ProveedorTelefono
                {
                    ProveedorId = id,
                    Telefono = item
                };
                proveedorAEditar.Telefonos.Add(nuevoTelefonoProveedor);
            }

            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
