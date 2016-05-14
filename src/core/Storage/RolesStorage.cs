using Bodegas.Db;
using Bodegas.Db.Entities;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Storage
{
    public class RolesStorage
    {
        private readonly BodegasContext db;

        public RolesStorage(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<RolResumen>> ObtenerTodos(PaginacionParametros paginacionParametros)
        {
            var query = db.Roles.Select(x => new RolResumen
            {
                Id = x.Id,
                Nombre = x.Nombre,
                CantidadUsuarios = x.Usuarios.Count
            }).OrderBy(x => x.Nombre);

            var elementos = await query.ToArrayAsync();
            return new PaginacionResultado<RolResumen>
            {
                Elementos = elementos
            };
        }
    }
}
