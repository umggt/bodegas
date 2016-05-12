using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Criptografia
{
    public static class StringExtensions
    {

        public static byte[] Encriptar(this string clave)
        {
            var encriptador = new EncriptadorDeClaves();
            return encriptador.Encriptar(clave);
        }

        public static bool Comparar(this string clave, byte[] claveEncriptada)
        {
            var encriptador = new EncriptadorDeClaves();
            return encriptador.Comparar(clave, claveEncriptada);
        }
    }
}
