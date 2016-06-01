using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class EgresoProductoMapping
    {
        internal static ModelBuilder MapearEgresoProducto(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EgresoProducto>(egresoProducto =>
            {
                // esta relacion no es necesaria ya que se define en el
                // mapping del egreso, pero igual no afecta en nada si 
                // se define aquí nuevamente.
                egresoProducto.HasOne(x => x.Egreso)
                    .WithMany(x => x.Productos)
                    .HasPrincipalKey(e => e.Id)
                    .HasForeignKey(e => e.EgresoId)
                    .OnDelete(DeleteBehavior.Cascade);

                egresoProducto.HasOne(x => x.Producto)
                    .WithMany()
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(e => e.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);

                egresoProducto.HasOne(x => x.UnidadDeMedida)
                    .WithMany()
                    .HasPrincipalKey(u => u.Id)
                    .HasForeignKey(e => e.UnidadDeMedidaId)
                    .OnDelete(DeleteBehavior.Restrict);

                egresoProducto.HasOne(x => x.Marca)
                    .WithMany()
                    .HasPrincipalKey(x => x.Id)
                    .HasForeignKey(x => x.MarcaId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            return modelBuilder;
        }
    }
}
