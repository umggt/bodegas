using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class UsuarioDetalle
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string NombreCompleto { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Correo { get; set; }

        public bool CorreoVerificado { get; set; }

        public string SitioWeb { get; set; }

        public IDictionary<string, string[]> Atributos { get; set; }

        public IDictionary<int, string> Roles { get; set; }


    }

}
