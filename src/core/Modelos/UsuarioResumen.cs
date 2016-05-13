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
        public string Etiqueta { get; set; }
        public string Correo { get; set; }
    }
}
