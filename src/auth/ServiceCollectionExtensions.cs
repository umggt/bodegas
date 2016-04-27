using System.Security.Cryptography.X509Certificates;
using Bodegas.Auth.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bodegas.Auth
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthenticationServer(this IServiceCollection services, X509Certificate2 certificate)
        {
            var builder = services.AddIdentityServer(options =>
            {
                options.SiteName = "Bodegas Autenticación";
                options.SigningCertificate = certificate;
                options.RequireSsl = false;
            });

            builder.Services.AddCors();
            builder.AddInMemoryClients(Clients.Get());
            builder.AddInMemoryScopes(Scopes.Get());
            builder.AddInMemoryUsers(Users.Get());
        }
    }
}
