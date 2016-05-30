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
                Direccion = proveedor.Direccion,
                Telefonos = proveedor.Telefonos.Select(x => new ProveedorTelefonoDetalle { ProveedorId = x.ProveedorId, Telefono = x.Telefono }).ToList()
            };
        }

        public async Task<int> CrearAsync(ProveedorDetalle proveedor, int usuarioId)
        {
            var nombre = proveedor.Nombre.Trim().ToLowerInvariant();
            var nombreContacto = proveedor.NombreDeContacto?.Trim()?.ToLowerInvariant();
            var direccion = proveedor.Direccion?.Trim()?.ToLowerInvariant();
           
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

        private async Task<bool> ExisteTelefono(int idProveedor, long telefono)
        {
            return await db.ProveedorTelefonos.AnyAsync(x => x.ProveedorId == idProveedor && x.Telefono == telefono);
        }

        public async Task<ProveedorTelefono> CrearTelefonoAsync(int idProveedor, long numero)
        {
            if (await ExisteTelefono(idProveedor, numero))
            {
                throw new InvalidOperationException($"Ya existe el número de teléfono {numero} para el proveedor {idProveedor}");
            }

            var nuevoTelefono = new ProveedorTelefono
            {
                ProveedorId = idProveedor,
                Telefono = numero
            };

            db.ProveedorTelefonos.Add(nuevoTelefono);

            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevoTelefono;
            }

            return null;

        }

        public async Task<bool> EliminarTelefonoAsync(int idProveedor, long telefono)
        {
            var telefonoAEliminar = await db.ProveedorTelefonos.SingleOrDefaultAsync(x => x.Telefono == telefono && x.ProveedorId == idProveedor);

            if (telefonoAEliminar == null)
            {
                throw new RegistroNoEncontradoException($"No existe el teléfono {telefono}");
            }

            db.ProveedorTelefonos.Remove(telefonoAEliminar);

            var filasAfectadas = await db.SaveChangesAsync();
            return filasAfectadas > 0;
        }


    }
}
