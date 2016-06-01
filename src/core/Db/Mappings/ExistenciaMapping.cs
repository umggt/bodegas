using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ExistenciaMapping
    {
        internal static ModelBuilder MapearExistencia(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Existencia>(existencia =>
            {
                //existencia.HasAlternateKey(x => x.ProductoId);

                existencia.HasOne(x => x.Producto)
                    .WithMany()
                    .HasForeignKey(e => e.ProductoId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                existencia.HasMany(x => x.Cantidades)
                    .WithOne(x => x.Existencia)
                    .HasForeignKey(x => x.ExistenciaId)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            return modelBuilder;
        }
    }
}
