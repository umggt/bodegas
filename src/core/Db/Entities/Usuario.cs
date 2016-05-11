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
        [MaxLength(200)]
        public byte[] Clave { get; set; }

        [Required]
        [StringLength(400)]
        public string Etiqueta { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombres { get; set; }

        [StringLength(200)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        public bool CorreoVerificado { get; set; }

        [StringLength(200)]
        public string SitioWeb { get; set; }

        public ICollection<UsuarioAtributo> Atributos { get; set; }

        public ICollection<UsuarioRol> Roles { get; set; }
    }
}
