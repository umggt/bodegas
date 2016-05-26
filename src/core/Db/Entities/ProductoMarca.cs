using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class ProductoMarca
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
    }
}
