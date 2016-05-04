using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    [Table("OpcionesDeMenu")]
    public class OpcionDeMenu
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(300)]
        public string Descripcion { get; set; }

        [StringLength(100)]
        public string Ruta { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        [StringLength(50)]
        public string Icono { get; set; }

        public int? OpcionPadreId { get; set; }

        public OpcionDeMenu OpcionPadre { get; set; }

        public ICollection<OpcionDeMenu> Opciones { get; set; }
    }
}
