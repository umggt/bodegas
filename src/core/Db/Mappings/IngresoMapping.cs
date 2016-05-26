using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class IngresoMapping
    {
        internal static ModelBuilder MapearIngreso(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingreso>(ingreso =>
            {
                ingreso.HasOne(x => x.Usuario)
                    .WithMany()
                    .HasForeignKey(i => i.UsuarioId)
                    .HasPrincipalKey(u => u.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                ingreso.HasOne(x => x.Proveedor)
                    .WithMany()
                    .HasForeignKey(i => i.ProveedorId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                ingreso.HasOne(x => x.Bodega)
                    .WithMany()
                    .HasForeignKey(i => i.BodegaId)
                    .HasPrincipalKey(b => b.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                ingreso.HasMany(x => x.Productos)
                    .WithOne(p => p.Ingreso)
                    .HasForeignKey(p => p.IngresoId)
                    .HasPrincipalKey(i => i.Id)
                    .OnDelete(DeleteBehavior.Cascade);

            });
            return modelBuilder;
        }
    }
}
