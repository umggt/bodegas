using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class IngresoProductoDetalle
    {
        public int Id { get; set; }
        public int IngresoId { get; set; }
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int UnidadId { get; set; }
        public string UnidadNombre { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int MarcaId { get; set; }
        public string MarcaNombre { get; set; }
        public string Serie { get; set; }
        public IngresoProductoCaracteristicaDetalle[] Caracteristicas { get; set; }
    }
}
