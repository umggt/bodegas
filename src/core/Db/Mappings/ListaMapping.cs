using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ListaMapping
    {
        internal static ModelBuilder MapearLista(this ModelBuilder builder)
        {
            builder.Entity<Lista>(lista =>
            {
                lista.HasAlternateKey(x => x.Nombre);

                lista.HasMany(x => x.Valores)
                    .WithOne(x => x.Lista)
                    .HasForeignKey(lv => lv.ListaId)
                    .HasPrincipalKey(l => l.Id)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            return builder;
        }
    }
}
