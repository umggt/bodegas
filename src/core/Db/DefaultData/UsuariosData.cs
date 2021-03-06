﻿using System.Linq;
using Bodegas.Criptografia;
using Bodegas.Db.Entities;

namespace Bodegas.Db.DefaultData
{
    internal static class UsuariosData
    {
        internal static BodegasContext GenerarUsuarios(this BodegasContext db)
        {
            if (db.Usuarios.Any())
            {
                return db;
            }

            var alice = new Usuario
            {
                Login = "alice",
                Clave = "alice".Encriptar(),
                NombreCompleto = "Alice Smith",
                Nombres = "Alice",
                Apellidos = "Smith",
                Correo = "alicesmith@email.com",
                CorreoVerificado = true,
                SitioWeb = "http://alice.com",
                Activo = true,
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
                NombreCompleto = "Bob Smith",
                Nombres = "Bob",
                Apellidos = "Smith",
                Correo = "bobsmith@email.com",
                CorreoVerificado = true,
                SitioWeb = "http://bob.com",
                Activo = true,
                Atributos = new[]
                {
                    new UsuarioAtributo { Nombre = "street_address", Valor = "One Hacker Way" },
                    new UsuarioAtributo { Nombre = "postal_code", Valor = "69118" },
                    new UsuarioAtributo { Nombre = "country", Valor = @"Germany" }

                }
            };

            db.Usuarios.Add(alice);
            db.Usuarios.Add(bob);
            db.SaveChanges();

            return db;
        }
    }
}
