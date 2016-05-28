using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Bodegas.Constantes.MensajesDeError;

namespace Bodegas.Modelos
{
    public class ListaDetalle
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(100, ErrorMessage = StringLengthMessage)]
        public string Nombre { get; set; }

        public ICollection<ListaValorDetalle> Valores { get; set; }

    }
}
