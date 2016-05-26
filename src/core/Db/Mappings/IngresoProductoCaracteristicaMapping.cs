using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db.Mappings
{
    internal static class IngresoProductoCaracteristicaMapping
    {
        internal static ModelBuilder MapearIngresoProductoCaracteristica(this ModelBuilder builder)
        {
            builder.Entity<IngresoProductoCaracteristica>(ingresoProductoCaracteristica =>
            {

                ingresoProductoCaracteristica.HasOne(x => x.IngresoProducto)
                    .WithMany(x => x.Caracteristicas)
                    .HasForeignKey(ipc => ipc.IngresoProductoId)
                    .HasPrincipalKey(ip => ip.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                ingresoProductoCaracteristica.HasOne(x => x.Caracteristica)
                    .WithMany()
                    .HasForeignKey(ipc => ipc.CaracteristicaId)
                    .HasPrincipalKey(pc => pc.Id)
                    .OnDelete(DeleteBehavior.Restrict);

                ingresoProductoCaracteristica.HasOne(x => x.ListaValor)
                    .WithMany()
                    .HasForeignKey(ipc => ipc.ListaValorId)
                    .HasPrincipalKey(lv => lv.Id)
                    .OnDelete(DeleteBehavior.SetNull);

            });
            return builder;
        }
    }
}
