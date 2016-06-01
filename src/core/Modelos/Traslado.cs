using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class Traslado
    {
        public int EgresoId { get; internal set; }
        public int IngresoId { get; internal set; }
        public int BodegaDestinoId { get; set; }
        public int BodegaOrigenId { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<ProductoEgresoDetalle> Productos { get; set; }
    }
}
