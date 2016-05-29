using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Bodegas.Constantes.MensajesDeError;

namespace Bodegas.Modelos
{
    public class ProveedorDetalle
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(400, ErrorMessage = StringLengthMessage)]
        public string Nombre { get; set; }

        [Display(Name = "Direccion")]
        [StringLength(1000, ErrorMessage = StringLengthMessage)]
        public string Direccion { get; set; }

        [Display(Name = "Nombre de Contacto")]
        [StringLength(1000, ErrorMessage = StringLengthMessage)]
        public string NombreDeContacto { get; set; }

        public int[] Telefonos { get; set; }
    }
}
