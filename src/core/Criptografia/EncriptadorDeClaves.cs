using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Bodegas.Criptografia
{
    public class EncriptadorDeClaves
    {
        public byte[] GenerarSalt(int longitud)
        {
            var bytes = new byte[longitud];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        public byte[] GenerarHash(string clave, byte[] salt, int iterations, int longitud)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(clave, salt, iterations))
            {
                return deriveBytes.GetBytes(longitud);
            }
        }
    }
}
