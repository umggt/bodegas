using System.ComponentModel.DataAnnotations;

namespace Bodegas.Db.Entities
{
    public class UsuarioAtributo
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(3000)]
        public string Valor { get; set; }
    }
}