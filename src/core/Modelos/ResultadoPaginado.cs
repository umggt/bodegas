using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class PaginacionResultado<T>
    {
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalElementos { get; set; }
        public int CantidadElementos { get; set; }
        public int ElementosPorPagina { get; set; }
        public int? PaginaAnterior { get; set; }
        public int? PaginaSiguiente { get; set; }
        public int[] Paginas { get; set; }
        public T[] Elementos { get; set; }
    }
}
