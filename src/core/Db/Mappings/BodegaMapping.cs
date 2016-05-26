using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Mappings
{
    internal static class BodegaMapping
    {
        internal static ModelBuilder MapearBodega(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bodega>(bodega =>
            {
                // Crear un unique constraint sobre la propiedad nombre
                // para prevenir bodegas con el mismo nombre.
                bodega.HasAlternateKey(u => u.Nombre);

                bodega.HasOne(b => b.UsuarioCreacion).WithMany().HasPrincipalKey(u => u.Id).HasForeignKey(b => b.UsuarioCreacionId);
                bodega.HasOne(b => b.UsuarioModifica).WithMany().HasForeignKey(u => u.Id).HasForeignKey(b => b.UsuarioModificaId);

            });

            return modelBuilder;
        }
    }
}
