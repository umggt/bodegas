using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ProductoMapping
    {
        internal static ModelBuilder MapearProducto(this ModelBuilder builder)
        {
            builder.Entity<Producto>(producto =>
            {
                producto.HasMany(x => x.Caracteristicas)
                    .WithOne(x => x.Producto)
                    .HasForeignKey(pc => pc.ProductoId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                producto.HasMany(x => x.Marcas)
                    .WithOne(x => x.Producto)
                    .HasForeignKey(pm => pm.ProductoId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                producto.HasMany(x => x.UnidadesDeMedida)
                    .WithOne(x => x.Producto)
                    .HasForeignKey(pum => pum.ProductoId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            return builder;
        }
    }
}
