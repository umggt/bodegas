using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class PaginacionParametros
    {
        public int Pagina { get; set; }
        public int ElementosPorPagina { get; set; }
        public IDictionary<string, bool> Ordenamiento { get; set; }
    }
}
