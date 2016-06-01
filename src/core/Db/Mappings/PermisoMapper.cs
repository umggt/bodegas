using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;

namespace Bodegas.Db.Mappings
{
    internal static class PermisoMapper
    {
        internal static ModelBuilder MapearPermiso(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permiso>(permiso => {
                //permiso.HasAlternateKey(p => p.Nombre);
            });
            return modelBuilder;
        }

    }
}
