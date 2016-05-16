using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class UsuarioDetalle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(400)]
        public string NombreCompleto { get; set; }

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
        [DataType(DataType.Url)]
        public string SitioWeb { get; set; }

        public bool Activo { get; set; }

        public IDictionary<string, string[]> Atributos { get; set; }

        public IDictionary<int, string> Roles { get; set; }


    }

}
