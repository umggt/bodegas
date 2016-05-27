using System.ComponentModel.DataAnnotations;
using static Bodegas.Constantes.MensajesDeError;

namespace Bodegas.Modelos
{
    public class ProductoDetalle
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(100, ErrorMessage = StringLengthMessage)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(5000, ErrorMessage = StringLengthMessage)]
        public string Descripcion { get; set; }

    }
}
