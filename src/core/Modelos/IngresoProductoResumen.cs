using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class IngresoProductoResumen
    {
        public string Producto { get; set; }
        public string Marca { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int Id { get; set; }
        public int IngresoId { get; set; }
    }
}
