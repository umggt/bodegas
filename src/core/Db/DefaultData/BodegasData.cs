using Bodegas.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.DefaultData
{
    internal static class BodegasData
    {
        internal static BodegasContext GenerarBodegas(this BodegasContext db)
        {
            if (db.Bodegas.Any())
            {
                return db;
            }

            if (!db.Usuarios.Any())
            {
                return db;
            }

            db.Bodegas.AddRange(new[] {
                new Bodega {
                    Nombre = "Bodega Central",
                    Direccion = "Zona 1",
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    UsuarioCreacion = db.Usuarios.First(),
                    UsuarioModifica = db.Usuarios.First()
                },
                new Bodega {
                    Nombre = "Sucursal 1",
                    Direccion = "Zona 10",
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow,
                    UsuarioCreacion = db.Usuarios.First(),
                    UsuarioModifica = db.Usuarios.First()
                }
            });

            db.SaveChanges();

            return db;
        }
    }
}
