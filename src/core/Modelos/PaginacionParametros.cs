using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Bodegas.Modelos
{
    public class PaginacionParametros
    {
        public int Pagina { get; set; }
        public int ElementosPorPagina { get; set; }
        public IDictionary<string, bool> Ordenamiento { get; set; }

        public PaginacionParametros()
        {
            
        }

        public PaginacionParametros(int pagina = 1, int elementos = 20, string ordenamiento = null)
        {
            ExtractValues(pagina, elementos, ordenamiento);
        }


        internal PaginacionParametros(HttpRequest request)
        {
            int pagina;
            int elementos;
            string ordenamiento = request.Query["ordenamiento"];

            if (!int.TryParse(request.Query["pagina"], out pagina))
            {
                pagina = 1;
            }

            if (!int.TryParse(request.Query["elementos"], out elementos))
            {
                elementos = 20;
            }

            ExtractValues(pagina, elementos, ordenamiento);
        }

        private void ExtractValues(int pagina, int elementos, string ordenamiento)
        {
            Pagina = pagina;
            ElementosPorPagina = elementos;
            Ordenamiento =
                ordenamiento?.Split(',')?
                    .Select(x => new { order = !x.StartsWith("-"), field = x.TrimStart('-') })?
                    .GroupBy(x => x.field)?
                    .ToDictionary(x => x.Key, x => x.First().order);
        }

    }
}
