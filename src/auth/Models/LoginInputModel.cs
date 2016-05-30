using System.ComponentModel.DataAnnotations;

namespace Bodegas.Auth.Models
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string SignInId { get; set; }
    }
}