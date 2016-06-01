using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class EgresoProducto
    {
        public int Id { get; set; }
        public int EgresoId { get; set; }
        public Egreso Egreso { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public int UnidadDeMedidaId { get; set; }
        public UnidadDeMedida UnidadDeMedida { get; set; }
        public decimal Cantidad { get; set; }
    }
}
