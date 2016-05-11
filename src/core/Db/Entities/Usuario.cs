using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [MaxLength(1024)]
        public byte[] Password { get; set; }

        public ICollection<UsuarioAtributo> Atributos { get; set; }
    }
}
