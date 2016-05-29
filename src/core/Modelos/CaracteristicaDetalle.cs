using Bodegas.Db.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class CaracteristicaDetalle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Nombre Caracteristica")]
        public string Nombre { get; set; }

        public int Tipo { get; set; }

        public string TipoNombre { get; set; }

        public int? ListaId { get; set; }

        public decimal? Minimo { get; set; }

        public decimal? Maximo { get; set; }

        public bool Requerido { get; set; }

    }
}
