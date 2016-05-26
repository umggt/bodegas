using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class ExistenciaCantidad
    {
        public int ExistenciaId { get; set; }
        public Existencia Existencia { get; set; }

        public int UnidadDeMedidaId { get; set; }
        public UnidadDeMedida UnidadDeMedida { get; set; }

        public decimal Cantidad { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
