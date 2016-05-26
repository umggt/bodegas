using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    /// <summary>
    /// Unidades de medida que pueden aplicarse a un producto.
    /// </summary>
    public class ProductoUnidadDeMedida
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int UnidadDeMedidaId { get; set; }
        public UnidadDeMedida UnidadDeMedida { get; set; }
    }
}
