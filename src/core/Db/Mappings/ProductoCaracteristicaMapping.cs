using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ProductoCaracteristicaMapping
    {
        internal static ModelBuilder MapearProductoCaracteristia(this ModelBuilder builder)
        {
            builder.Entity<ProductoCaracteristica>(productoCaracteristica =>
            {
                productoCaracteristica.HasOne(x => x.Producto)
                    .WithMany(x => x.Caracteristicas)
                    .HasForeignKey(pc => pc.ProductoId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                productoCaracteristica.HasOne(x => x.Lista)
                    .WithMany()
                    .HasForeignKey(pc => pc.ListaId)
                    .HasPrincipalKey(l => l.Id)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            return builder;
        }
    }
}
