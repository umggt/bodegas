using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ProveedorProductoMapping
    {
        internal static ModelBuilder MapearProveedorProducto(this ModelBuilder builder)
        {
            builder.Entity<ProveedorProducto>(proveedorProducto =>
            {

                proveedorProducto.HasKey(x => new {x.ProveedorId, x.ProductoId});

                proveedorProducto.HasOne(x => x.Proveedor)
                    .WithMany(x => x.Productos)
                    .HasForeignKey(x => x.ProveedorId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                proveedorProducto.HasOne(x => x.Producto)
                    .WithMany()
                    .HasForeignKey(x => x.ProductoId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            return builder;
        }
    }
}
