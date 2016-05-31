using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class EgresoResumen
    {
        public int Id { get; set; }
        public string Bodega { get; set; }
        public DateTime Fecha { get; set; }
    }
}
