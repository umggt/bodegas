using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;

namespace Bodegas.Db.Mappings
{
    internal static class RolMapper
    {
        internal static ModelBuilder MapearRol(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>(rol => rol.HasAlternateKey(r => r.Nombre));
            return modelBuilder;
        }
    }
}
