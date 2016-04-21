using System.Collections.Generic;
using IdentityServer4.Core.Models;

namespace Bodegas.Auth.Configuration
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.ProfileAlwaysInclude,
                StandardScopes.EmailAlwaysInclude,
                StandardScopes.OfflineAccess,
                StandardScopes.RolesAlwaysInclude,

                new Scope
                {
                    Name = "bodegas.api",
                    DisplayName = "Bodegas REST API",
                    Description = "Bodegas REST API",
                    Type = ScopeType.Resource
                }
            };
        }
    }
}