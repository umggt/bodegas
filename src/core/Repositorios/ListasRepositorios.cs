using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Bodegas.Modelos;


namespace Bodegas.Repositorios
{
    public class ListasRepositorio
    {
        private readonly BodegasContext db;

        public ListasRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<ListaResumen>> ObtenerTodasAsync(PaginacionParametros paginacion)
        {
            var query = from n in db.Listas
                        select new ListaResumen
                        {
                            Id = n.Id,                           
                            Nombre = n.Nombre                           
                        };

            // Aqui se mapean los paremetros que envía la UI a sus respectivas propiedad
            // para poder realizar el ordenamiento.
            // Por ejemplo, si la UI envía ordenamiento=login,-nombre este mapping
            // identifica que "login" corresponde a la propiedad "Login" de UsuarioResumen
            // y que "nombre" corresponde a la propiedad "Nombre" de UsuarioResumen.
            var orderMapping = new OrdenMapping<ListaResumen> {
                { "id", x => x.Id },              
                { "nombre", x => x.Nombre }            
            };

            var result = await query.OrdenarAsync(paginacion, x => x.Id, orderMapping);

            return result;
        }

    }
}
