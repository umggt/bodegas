using IdentityServer4.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Core.Models;
using Bodegas.Db;
using IdentityServer4.Core.Extensions;
using Microsoft.Data.Entity;
using System.Security.Claims;
using IdentityModel;

namespace Bodegas.Auth.Services
{
    public class ProfileService : IProfileService
    {
        private readonly BodegasContext db;

        public ProfileService(BodegasContext db)
        {
            this.db = db;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.Subject == null) throw new ArgumentNullException("subject");

            var usuarioId = int.Parse(context.Subject.GetSubjectId());
            var usuario = await db.Usuarios.Include(x => x.Atributos).SingleAsync(u => u.Id == usuarioId);

            var claims = new List<Claim>{
                new Claim(JwtClaimTypes.Subject, usuario.Id.ToString()),
                new Claim(JwtClaimTypes.Name, usuario.Etiqueta),
                new Claim(JwtClaimTypes.GivenName, usuario.Nombres),
                new Claim(JwtClaimTypes.FamilyName, usuario.Apellidos),
                new Claim(JwtClaimTypes.Email, usuario.Correo),
                new Claim(JwtClaimTypes.EmailVerified, usuario.CorreoVerificado.ToString(), ClaimValueTypes.Boolean),
                new Claim(JwtClaimTypes.WebSite, usuario.SitioWeb)
            };

            foreach (var atributo in usuario.Atributos)
            {
                claims.Add(new Claim(atributo.Nombre, atributo.Valor));
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            if (context.Subject == null) throw new ArgumentNullException("subject");
            var usuarioId = int.Parse(context.Subject.GetSubjectId());
            var usuario = await db.Usuarios.SingleOrDefaultAsync(u => u.Id == usuarioId);
            // TODO: context.IsActive = usuario != null && usuairo.Activo;
            context.IsActive = usuario != null;
        }
    }
}
