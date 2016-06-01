using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class IngresoDetalle
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public int BodegaId { get; set; }
        public string BodegaNombre { get; set; }
        public IngresoProductoDetalle[] Productos { get; set; }
    }
}
