using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ProductoUnidadDeMedidaMapping
    {
        internal static ModelBuilder MapearProductoUnidadDeMedida(this ModelBuilder builder)
        {
            builder.Entity<ProductoUnidadDeMedida>(productoUnidadDeMedida =>
            {

                productoUnidadDeMedida.HasKey(x => new {x.ProductoId, x.UnidadDeMedidaId});

                productoUnidadDeMedida.HasOne(x => x.Producto)
                    .WithMany(x => x.UnidadesDeMedida)
                    .HasForeignKey(x => x.ProductoId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                productoUnidadDeMedida.HasOne(x => x.UnidadDeMedida)
                    .WithMany()
                    .HasForeignKey(x => x.UnidadDeMedidaId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            return builder;
        }
    }
}
