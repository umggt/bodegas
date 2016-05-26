using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class ProveedorTelefono
    {
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public long Telefono { get; set; }
    }
}
