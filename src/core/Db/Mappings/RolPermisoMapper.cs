using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class RolPermisoMapper
    {
        internal static ModelBuilder MapearRolPermiso(this ModelBuilder modelBuilder)
        {
            // Relación de muchos a muchos entre roles y permisos
            modelBuilder.Entity<RolPermiso>(rolPermiso =>
            {
                rolPermiso.HasKey(rp => new {rp.RolId, rp.PermisoId});

                // Un rol tiene muchos permisos
                rolPermiso.HasOne(rp => rp.Rol)
                    .WithMany(r => r.Permisos)
                    .HasForeignKey(rp => rp.RolId)
                    .HasPrincipalKey(r => r.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                // Un permiso puede estar en muchos roles
                rolPermiso.HasOne(rp => rp.Permiso)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(rp => rp.PermisoId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            return modelBuilder;
        }
    }
}
