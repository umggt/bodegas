using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ProveedorTelefonoMapping
    {
        internal static ModelBuilder MapearProveedorTelefono(this ModelBuilder builder)
        {
            builder.Entity<ProveedorTelefono>(proveedorTelefono =>
            {

                proveedorTelefono.HasKey(x => new {x.ProveedorId, x.Telefono});

                proveedorTelefono.HasOne(x => x.Proveedor)
                    .WithMany(x => x.Telefonos)
                    .HasForeignKey(x => x.ProveedorId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            return builder;
        }
    }
}
