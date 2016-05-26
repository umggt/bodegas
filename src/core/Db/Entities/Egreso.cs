using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class Egreso
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int BodegaId { get; set; }
        public Bodega Bodega { get; set; }

        public ICollection<EgresoProducto> Productos { get; set; }

        public Egreso()
        {
            Fecha = DateTime.UtcNow;
        }
    }
}
