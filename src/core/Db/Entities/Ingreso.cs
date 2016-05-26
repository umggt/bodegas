using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class Ingreso
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int BodegaId { get; set; }
        public Bodega Bodega { get; set; }

        public ICollection<IngresoProducto> Productos { get; set; }

        public Ingreso()
        {
            Fecha = DateTime.UtcNow;
        }
    }
}
