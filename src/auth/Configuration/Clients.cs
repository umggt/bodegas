using System.Collections.Generic;
using IdentityServer4.Core.Models;

namespace Bodegas.Auth.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                ///////////////////////////////////////////
                // JS OIDC Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "bodegas_app",
                    ClientName = "Cliente JavaScript de Bodegas",
                    ClientUri = "http://localhost:63556/",
                    RequireConsent = false,
                    Flow = Flows.Implicit,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:63556/",
                        "http://localhost:63556/silent_renew.html",
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:63556/logedout.html",
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:63556/"
                    },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.Email.Name,
                        StandardScopes.Roles.Name,
                        "bodegas.api"
                    }
                },
            };
        }
    }
}