using System.ComponentModel.DataAnnotations;

namespace Bodegas.Auth.Models
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string SignInId { get; set; }
    }
}