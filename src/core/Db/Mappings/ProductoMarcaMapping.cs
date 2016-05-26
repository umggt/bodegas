using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ProductoMarcaMapping
    {
        internal static ModelBuilder MapearProductoMarca(this ModelBuilder builder)
        {
            builder.Entity<ProductoMarca>(productoMarca =>
            {
                productoMarca.HasKey(x => new {x.ProductoId, x.MarcaId});

                productoMarca.HasOne(x => x.Producto)
                    .WithMany(x => x.Marcas)
                    .HasForeignKey(pm => pm.ProductoId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                productoMarca.HasOne(x => x.Marca)
                    .WithMany(x => x.Productos)
                    .HasForeignKey(pm => pm.MarcaId)
                    .HasPrincipalKey(m => m.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            return builder;
        }
    }
}
