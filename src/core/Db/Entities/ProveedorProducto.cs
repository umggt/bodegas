using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class ProveedorProducto
    {
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
