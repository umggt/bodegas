using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Bodegas.Constantes.MensajesDeError;

namespace Bodegas.Modelos
{
    public class ListaValorDetalle
    {
        public int Id { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(100, ErrorMessage = StringLengthMessage)]
        public string Valor { get; set; }

    }
}
