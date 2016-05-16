using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Constants
{
    public static class ErrorMessages
    {
        public const string UrlMessage = "El campo {0} no es una url http, https, o ftp formada completamente.";
        public const string StringLengthMessage = "El campo {0} permite una logitud máxima de {1} caracteres.";
        public const string RequiredMessage = "El campo {0} es requerido.";
        public const string EmailMessage = "El campo {0} no es una dirección de correo válida.";
    }
}
