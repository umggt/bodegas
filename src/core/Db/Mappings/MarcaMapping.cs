using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class MarcaMapping
    {
        internal static ModelBuilder MapearMarca(this ModelBuilder builder)
        {
            builder.Entity<Marca>(marca =>
            {
                marca.HasMany(x => x.Productos)
                    .WithOne(x => x.Marca)
                    .HasForeignKey(pm => pm.MarcaId)
                    .HasPrincipalKey(m => m.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            return builder;
        }
    }
}
