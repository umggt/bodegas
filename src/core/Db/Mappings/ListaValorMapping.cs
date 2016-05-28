using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class ListaValorMapping
    {
        internal static ModelBuilder MapearListaValor(this ModelBuilder builder)
        {
            builder.Entity<ListaValor>(listaValor =>
            {
                //listaValor.HasAlternateKey(x => new {x.ListaId, x.Valor});

                listaValor.HasOne(x => x.Lista)
                    .WithMany(x => x.Valores)
                    .HasForeignKey(lv => lv.ListaId)
                    .HasPrincipalKey(l => l.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            return builder;
        }
    }
}
