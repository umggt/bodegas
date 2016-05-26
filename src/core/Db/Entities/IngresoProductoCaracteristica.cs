using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class IngresoProductoCaracteristica
    {
        public int Id { get; set; }
        public int IngresoProductoId { get; set; }
        public IngresoProducto IngresoProducto { get; set; }
        public int CaracteristicaId { get; set; }
        public ProductoCaracteristica Caracteristica { get; set; }
        public string Valor { get; set; }

        // Si la caracteristica es tipo Lista, deberá seleccionar un valor id.
        public int? ListaValorId { get; set; }
        public ListaValor ListaValor { get; set; }
    }
}
