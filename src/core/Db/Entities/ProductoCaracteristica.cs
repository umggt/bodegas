using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class ProductoCaracteristica
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        public ProductoTipoCaracteristica TipoCaracteristica { get; set; }

        public int? ListaId { get; set; }

        public Lista Lista { get; set; }

        public decimal? Minimo { get; set; }

        public decimal? Maximo { get; set; }

        public bool Requerido { get; set; }

        public string ExpresionDeValidacion { get; set; }
    }
}
