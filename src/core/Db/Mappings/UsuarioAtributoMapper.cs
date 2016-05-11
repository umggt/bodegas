using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;

namespace Bodegas.Db.Mappings
{
    internal static class UsuarioAtributoMapper
    {
        internal static ModelBuilder MapearUsuarioAtributo(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioAtributo>(
                usuarioAtributo => { usuarioAtributo.HasIndex(a => new { a.UsuarioId, a.Nombre }); });
            return modelBuilder;
        }
    }
}
