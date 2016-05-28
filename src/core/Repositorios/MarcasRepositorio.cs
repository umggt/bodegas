using Bodegas.Db;
using Bodegas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Repositorios
{
    public class MarcasRepositorio
    {
        private readonly BodegasContext db;

        public MarcasRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<MarcaResumen>> ObtenerTodasAsync(PaginacionParametros paginacion)
        {
            var query = from q in db.Marcas
                        select new MarcaResumen
                        {
                            Id = q.Id,
                            Nombre = q.Nombre
                        };

            return await query.OrdenarAsync(paginacion, x => x.Id);
        }
    }
}
