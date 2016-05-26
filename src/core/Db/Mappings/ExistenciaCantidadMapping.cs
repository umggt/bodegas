using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ExistenciaCantidadMapping
    {
        internal static ModelBuilder MapearExistenciaCantidad(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ExistenciaCantidad>(existenciaCantidad =>
            {
                existenciaCantidad.HasKey(x => new {x.ExistenciaId, x.UnidadDeMedidaId});

                existenciaCantidad.HasOne(x => x.Existencia)
                    .WithMany()
                    .HasForeignKey(ec => ec.ExistenciaId)
                    .HasPrincipalKey(e => e.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                existenciaCantidad.HasOne(x => x.UnidadDeMedida)
                    .WithMany()
                    .HasForeignKey(ec => ec.UnidadDeMedidaId)
                    .HasPrincipalKey(u => u.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            return modelBuilder;
        }
    }
}
