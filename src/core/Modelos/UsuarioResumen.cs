using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class UsuarioResumen
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
    }
}
