using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;

namespace Bodegas.Db.Mappings
{
    internal static class OpcionDeMenuMapper
    {
        internal static ModelBuilder MapearOpcionesDeMenu(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OpcionDeMenu>(opcion =>
            {
                opcion.HasOne(x => x.OpcionPadre)
                    .WithMany(x => x.Opciones)
                    .HasForeignKey(x => x.OpcionPadreId);
            });

            return modelBuilder;
        }
    }
}
