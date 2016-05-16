using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Exceptions
{
    public class RegistroNoEncontradoException : Exception
    {
        public RegistroNoEncontradoException(string message) : base(message)
        {

        }
    }
}
