using System.Threading.Tasks;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bodegas.Auth.Models
{
    public class ConsentResult : IActionResult
    {
        private readonly string requestId;
        private readonly ConsentResponse response;

        public ConsentResult(string requestId, ConsentResponse response)
        {
            this.requestId = requestId;
            this.response = response;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var interaction = context.HttpContext.RequestServices.GetRequiredService<ConsentInteraction>();
            await interaction.ProcessResponseAsync(requestId, response);
        }
    }
}
