using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;

namespace Bodegas.Db.DefaultData
{
    internal static class RolData
    {
        internal static BodegasContext GenerarRoles(this BodegasContext db)
        {
            if (db.Roles.Any())
            {
                return db;
            }

            var administrador = db.CrearRolConUnUsuario("Administrador", "alice");
            var operador = db.CrearRolConUnUsuario("Operador", "bob");

            db.Roles.Add(administrador);
            db.Roles.Add(operador);
            db.SaveChanges();
            return db;
        }

        private static Rol CrearRol(this BodegasContext db, string nombreDelRol)
        {
            return db.CrearRolConUnUsuario(nombreDelRol, loginDelUsuario: null);
        }

        private static Rol CrearRolConUnUsuario(this BodegasContext db, string nombreDelRol, string loginDelUsuario)
        {
            var rol = new Rol
            {
                Nombre = nombreDelRol
            };

            if (loginDelUsuario == null) return rol;

            var usuario = db.Usuarios.FirstOrDefault(x => x.Login == loginDelUsuario);
            if (usuario != null)
            {
                rol.Usuarios = new[] { new UsuarioRol { Usuario = usuario, Rol = rol } };
            }

            return rol;
        }
    }
}
