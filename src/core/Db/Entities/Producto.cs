using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(5000)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Caracteristicas que puede tener el producto
        /// </summary>
        public ICollection<ProductoCaracteristica> Caracteristicas { get; set; }
        
        /// <summary>
        /// Marcas que aplican para el producto
        /// </summary>
        public ICollection<ProductoMarca> Marcas { get; set; }

        /// <summary>
        /// Unidades de medida que aplican para el producto
        /// </summary>
        public ICollection<ProductoUnidadDeMedida> UnidadesDeMedida { get; set; }
    }
}
