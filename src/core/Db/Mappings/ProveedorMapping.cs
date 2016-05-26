using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ProveedorMapping
    {
        internal static ModelBuilder MapearProveedor(this ModelBuilder builder)
        {
            builder.Entity<Proveedor>(proveedor =>
            {
                proveedor.HasMany(x => x.Telefonos)
                    .WithOne(x => x.Proveedor)
                    .HasForeignKey(x => x.ProveedorId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                proveedor.HasMany(x => x.Productos)
                    .WithOne(x => x.Proveedor)
                    .HasForeignKey(x => x.ProveedorId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                proveedor.HasOne(x => x.UsuarioCreacion)
                    .WithMany()
                    .HasForeignKey(x => x.UsuarioCreacionId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                proveedor.HasOne(x => x.UsuarioModifica)
                    .WithMany()
                    .HasForeignKey(x => x.UsuarioModificaId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Restrict);

            });
            return builder;
        }
    }
}
