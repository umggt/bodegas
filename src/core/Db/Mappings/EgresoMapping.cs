using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class EgresoMapping
    {
        internal static ModelBuilder MapearEgreso(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Egreso>(egreso =>
            {
                egreso.HasOne(x => x.Usuario).WithMany().HasForeignKey(e => e.UsuarioId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.Restrict);
                egreso.HasOne(x => x.Bodega).WithMany().HasForeignKey(e => e.BodegaId).HasPrincipalKey(b => b.Id).OnDelete(DeleteBehavior.Cascade);
                egreso.HasMany(x => x.Productos).WithOne(x => x.Egreso).HasForeignKey(p => p.EgresoId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);

            });

            return modelBuilder;
        }

    }
}
