using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class IngresoProducto
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string NumeroDeSerie { get; set; }

        public int IngresoId { get; set; }
        public Ingreso Ingreso { get; set; }

        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int UnidadDeMedidaId { get; set; }
        public UnidadDeMedida UnidadDeMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public ICollection<IngresoProductoCaracteristica> Caracteristicas { get; set; }
    }
}
