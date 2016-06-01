using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class IngresoResumen
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Bodega { get; set; }
        public string Proveedor { get; set; }
        public IngresoProductoResumen[] Productos { get; set; }
    }
}
