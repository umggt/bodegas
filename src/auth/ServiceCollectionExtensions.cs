using System.Security.Cryptography.X509Certificates;
using Bodegas.Auth.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Core.Services.InMemory;
using System.Collections.Generic;
using IdentityServer4.Core.Services;
using Bodegas.Auth.Services;
using IdentityServer4.Core.Validation;

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
            builder.Services.AddTransient<IProfileService, ProfileService>();
            //builder.Services.AddTransient<IResourceOwnerPasswordValidator, InMemoryResourceOwnerPasswordValidator>();

            builder.AddInMemoryClients(Clients.Get());
            builder.AddInMemoryScopes(Scopes.Get());
        }
    }
}
