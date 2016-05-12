using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Bodegas.Criptografia
{
    public class EncriptadorDeClaves
    {
        private const int Iteraciones = 10;
        private const int LongitudSalt = 16;
        private const int LongitudHash = 184;

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

        public byte[] Encriptar(string clave)
        {
            var salt = GenerarSalt(LongitudSalt);
            var hash = GenerarHash(clave, salt, Iteraciones, LongitudHash);
            var result = new byte[LongitudSalt + LongitudHash];
            Array.Copy(salt, result, LongitudSalt);
            Array.Copy(hash, 0, result, LongitudSalt, LongitudHash);
            return result;
        }

        public bool Comparar(string clave, byte[] claveEncriptada)
        {
            var saltOriginal = new byte[LongitudSalt];

            Array.Copy(claveEncriptada, saltOriginal, LongitudSalt);

            var hash = GenerarHash(clave, saltOriginal, Iteraciones, LongitudHash);

            for (int i = 0; i < LongitudHash; i++)
            {
                if (hash[i] != claveEncriptada[i + LongitudSalt])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
