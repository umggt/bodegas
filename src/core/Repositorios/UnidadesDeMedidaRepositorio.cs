using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Bodegas.Db;

namespace Bodegas.Repositorios
{
    public class UnidadesDeMedidaRepositorio
    {
        private readonly BodegasContext db;

        public UnidadesDeMedidaRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<UnidadDeMedidaResumen>> ObtenerTodasAsync(PaginacionParametros paginacion)
        {
            var query = from q in db.UnidadesDeMedida
                        select new UnidadDeMedidaResumen
                        {
                            Id = q.Id,
                            Nombre = q.Nombre
                        };

            return await query.OrdenarAsync(paginacion, x => x.Id);
        }
    }
}
