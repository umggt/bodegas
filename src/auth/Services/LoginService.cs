using Bodegas.Criptografia;
using Bodegas.Db;
using Bodegas.Db.Entities;
using IdentityServer4.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Auth.Services
{
    public class LoginService
    {
        private readonly BodegasContext db;

        public LoginService(BodegasContext db)
        {
            this.db = db;
        }

        public bool ValidateCredentials(string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            
            var usuario = FindByUsername(username);

            if (usuario == null)
            {
                return false;
            }

            return password.Comparar(usuario.Clave);
        }

        public Usuario FindByUsername(string username)
        {
            return db.Usuarios.FirstOrDefault(u => u.Login.ToLowerInvariant() == username.ToLowerInvariant());
        }
    }
}
