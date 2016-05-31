using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.DefaultData
{
    internal static class ProveedoresData
    {
        internal static BodegasContext GenerarProveedores(this BodegasContext db) {
            if (db.Proveedores.Any())
            {
                return db;
            }

            if (!db.Usuarios.Any())
            {
                return db;
            }

            if (!db.Productos.Any())
            {
                return db;
            }

            db.Proveedores.AddRange(new[] {
                new Proveedor {
                    Nombre = "Grupo Sega, S.A.",
                    Direccion = "10 av. 30-57, zona 5. Guatemala, Guatemala.",
                    NombreDeContacto = "Lalo Landa",
                    Telefonos = new [] { new ProveedorTelefono { Telefono = 23845888 } },
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    UsuarioCreacion = db.Usuarios.First(),
                    UsuarioModifica = db.Usuarios.First(),
                    Productos = db.Productos.Select(x => new ProveedorProducto { Producto = x }).ToArray()
                },
                new Proveedor {
                    Nombre = "Canella, S.A.",
                    Direccion = "Boulevard los Proceres 17-66 zona 10",
                    NombreDeContacto = "Leon Kompowsky",
                    Telefonos = new [] { new ProveedorTelefono { Telefono = 23635090 } },
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    UsuarioCreacion = db.Usuarios.First(),
                    UsuarioModifica = db.Usuarios.First(),
                    Productos = db.Productos.Select(x => new ProveedorProducto { Producto = x }).ToArray()
                }
            });

            db.SaveChanges();
            return db;
        }
    }
}
