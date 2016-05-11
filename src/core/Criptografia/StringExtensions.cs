using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Criptografia
{
    public static class StringExtensions
    {
        public static byte[] Hash(this string clave)
        {
            var encriptador = new EncriptadorDeClaves();
            var salt = encriptador.GenerarSalt(16);
            var hash = encriptador.GenerarHash(clave, salt, 10, 184);
            var result = new byte[salt.Length + hash.Length];
            Array.Copy(salt, result, salt.Length);
            Array.Copy(hash, result, hash.Length);
            return result;
        }
    }
}
