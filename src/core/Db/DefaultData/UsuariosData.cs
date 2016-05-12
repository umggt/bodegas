using System.Linq;
using Bodegas.Criptografia;
using Bodegas.Db.Entities;

namespace Bodegas.Db.DefaultData
{
    internal static class UsuariosData
    {
        internal static BodegasContext GenerarUsuarios(this BodegasContext context)
        {
            if (context.Usuarios.Any())
            {
                return context;
            }

            var alice = new Usuario
            {
                Login = "alice",
                Clave = "alice".Encriptar(),
                Etiqueta = "Alice Smith",
                Nombres = "Alice",
                Apellidos = "Smith",
                Correo = "alicesmith@email.com",
                CorreoVerificado = true,
                SitioWeb = "http://alice.com",
                Atributos = new[]
                {
                    new UsuarioAtributo { Nombre = "street_address", Valor = "One Hacker Way" },
                    new UsuarioAtributo { Nombre = "postal_code", Valor = "69118" },
                    new UsuarioAtributo { Nombre = "country", Valor = @"Germany" }

                }
            };

            var bob = new Usuario
            {
                Login = "bob",
                Clave = "bob".Encriptar(),
                Etiqueta = "Bob Smith",
                Nombres = "Bob",
                Apellidos = "Smith",
                Correo = "bobsmith@email.com",
                CorreoVerificado = true,
                SitioWeb = "http://bob.com",
                Atributos = new[]
                {
                    new UsuarioAtributo { Nombre = "street_address", Valor = "One Hacker Way" },
                    new UsuarioAtributo { Nombre = "postal_code", Valor = "69118" },
                    new UsuarioAtributo { Nombre = "country", Valor = @"Germany" }

                }
            };

            context.Usuarios.Add(alice);
            context.Usuarios.Add(bob);
            context.SaveChanges();

            return context;
        }
    }
}
