using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Bodegas.Constants.ErrorMessages;

namespace Bodegas.Modelos
{
    public class UsuarioDetalle
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(100, ErrorMessage = StringLengthMessage)]
        public string Login { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(400, ErrorMessage = StringLengthMessage)]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(200, ErrorMessage = StringLengthMessage)]
        public string Nombres { get; set; }

        [StringLength(200, ErrorMessage = StringLengthMessage)]
        public string Apellidos { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = RequiredMessage)]
        [EmailAddress(ErrorMessage = EmailMessage)]
        [StringLength(200, ErrorMessage = StringLengthMessage)]
        public string Correo { get; set; }

        public bool CorreoVerificado { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Sitio Web")]
        [Url(ErrorMessage = UrlMessage)]
        [StringLength(200, ErrorMessage = StringLengthMessage)]
        public string SitioWeb { get; set; }

        public bool Activo { get; set; }

        public IDictionary<string, string[]> Atributos { get; set; }

        public IDictionary<int, string> Roles { get; set; }

    }

}
