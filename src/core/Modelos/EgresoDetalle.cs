using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class EgresoDetalle
    {
        public int EgresoId { get; set; }
        public int BodegaId { get; set; }
        public string UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<ProductoEgresoDetalle> Productos { get; set; }
       
    }
}
