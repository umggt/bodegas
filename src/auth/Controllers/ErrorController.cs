using System.Threading.Tasks;
using Bodegas.Auth.Models;
using IdentityServer4.Core;
using IdentityServer4.Core.Services;
using Microsoft.AspNet.Mvc;

namespace Bodegas.Auth.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ErrorInteraction errorInteraction;

        public ErrorController(ErrorInteraction errorInteraction)
        {
            this.errorInteraction = errorInteraction;
        }

        [Route(Constants.RoutePaths.Error, Name ="Error")]
        public async Task<IActionResult> Index(string id)
        {
            var vm = new ErrorViewModel();

            if (id != null)
            {
                var message = await errorInteraction.GetRequestAsync(id);
                if (message != null)
                {
                    vm.Error = message;
                }
            }

            return View("Error", vm);
        }
    }
}
