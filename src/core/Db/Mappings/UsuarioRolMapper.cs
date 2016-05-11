using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class UsuarioRolMapper
    {
        internal static ModelBuilder MapearUsuarioRol(this ModelBuilder modelBuilder)
        {
            // Relación de muchos a muchos entre usuarios y roles.
            modelBuilder.Entity<UsuarioRol>(usuarioRol =>
            {
                usuarioRol.HasKey(ur => new {ur.UsuarioId, ur.RolId});

                // Un usuario tiene muchos roles
                usuarioRol.HasOne(ur => ur.Usuario)
                    .WithMany(u => u.Roles)
                    .HasForeignKey(ur => ur.UsuarioId)
                    .HasPrincipalKey(u => u.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                // Un rol lo pueden tener muchos usuarios.
                usuarioRol.HasOne(ur => ur.Rol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(ur => ur.RolId)
                    .HasPrincipalKey(r => r.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            return modelBuilder;
        }
    }
}
