using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class IngresoProductoMapping
    {
        internal static ModelBuilder MapearIngresoProducto(this ModelBuilder builder)
        {
            builder.Entity<IngresoProducto>(ingresoProducto =>
            {
                ingresoProducto.HasAlternateKey(x => x.NumeroDeSerie);

                ingresoProducto.HasOne(x => x.Ingreso)
                    .WithMany(x => x.Productos)
                    .HasForeignKey(p => p.IngresoId)
                    .HasPrincipalKey(ip => ip.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                ingresoProducto.HasOne(x => x.Marca)
                    .WithMany()
                    .HasForeignKey(ip => ip.MarcaId)
                    .HasPrincipalKey(m => m.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                ingresoProducto.HasOne(x => x.Producto)
                    .WithMany()
                    .HasForeignKey(ip => ip.ProductoId)
                    .HasPrincipalKey(p => p.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                ingresoProducto.HasOne(x => x.UnidadDeMedida)
                    .WithMany()
                    .HasForeignKey(ip => ip.UnidadDeMedidaId)
                    .HasPrincipalKey(um => um.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                ingresoProducto.HasMany(x => x.Caracteristicas)
                    .WithOne(x => x.IngresoProducto)
                    .HasForeignKey(c => c.IngresoProductoId)
                    .HasPrincipalKey(ip => ip.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            return builder;
        }
    }
}
