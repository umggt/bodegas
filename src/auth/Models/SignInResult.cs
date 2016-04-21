using System.Threading.Tasks;
using IdentityServer4.Core.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bodegas.Auth.Models
{
    public class SignInResult : IActionResult
    {
        private readonly string requestId;

        public SignInResult(string requestId)
        {
            this.requestId = requestId;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var interaction = context.HttpContext.RequestServices.GetRequiredService<SignInInteraction>();
            await interaction.ProcessResponseAsync(requestId, new IdentityServer4.Core.Models.SignInResponse());
        }
    }
}
