using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bodegas
{
    public static class ClaimsPrincipalExtension
    {
        public static int Id(this ClaimsPrincipal user)
        {
            var idValue = user.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(idValue))
            {
                return -1;
            }

            int id;

            if (int.TryParse(idValue, out id))
            {
                return id;
            }

            return -2;
        }
    }
}
