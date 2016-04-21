using System.Linq;
using System.Threading.Tasks;
using Bodegas.Auth.Models;
using IdentityServer4.Core;
using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace Bodegas.Auth.Controllers
{
    public class ConsentController : Controller
    {
        private readonly ILogger<ConsentController> logger;
        private readonly IClientStore clientStore;
        private readonly ConsentInteraction consentInteraction;
        private readonly IScopeStore scopeStore;
        private readonly ILocalizationService localization;

        public ConsentController(
            ILogger<ConsentController> logger,
            ConsentInteraction consentInteraction,
            IClientStore clientStore,
            IScopeStore scopeStore,
            ILocalizationService localization)
        {
            this.logger = logger;
            this.consentInteraction = consentInteraction;
            this.clientStore = clientStore;
            this.scopeStore = scopeStore;
            this.localization = localization;
        }

        [HttpGet(Constants.RoutePaths.Consent, Name = "Consent")]
        public async Task<IActionResult> Index(string id)
        {
            var vm = await BuildViewModelAsync(id);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        [HttpPost(Constants.RoutePaths.Consent)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string button, string id, ConsentInputModel model)
        {
            if (button == "no")
            {
                return new ConsentResult(id, ConsentResponse.Denied);
            }
            else if (button == "yes" && model != null)
            {
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    return new ConsentResult(id, new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = model.ScopesConsented
                    });
                }
                else
                {
                    ModelState.AddModelError("", "You must pick at least one permission.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Selection");
            }

            var vm = await BuildViewModelAsync(id, model);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        async Task<IActionResult> BuildConsentResponse(string id, string[] scopesConsented, bool rememberConsent)
        {
            if (id != null)
            {
                var request = await consentInteraction.GetRequestAsync(id);
            }

            return View("Error");
        }

        async Task<ConsentViewModel> BuildViewModelAsync(string id, ConsentInputModel model = null)
        {
            if (id != null)
            {
                var request = await consentInteraction.GetRequestAsync(id);
                if (request != null)
                {
                    var client = await clientStore.FindClientByIdAsync(request.ClientId);
                    if (client != null)
                    {
                        var scopes = await scopeStore.FindScopesAsync(request.ScopesRequested);
                        var scopesArray = scopes as Scope[] ?? scopes.ToArray();
                        if (scopes != null && scopesArray.Any())
                        {
                            return new ConsentViewModel(model, id, request, client, scopesArray, localization);
                        }
                        else
                        {
                            logger.LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
                        }
                    }
                    else
                    {
                        logger.LogError("Invalid client id: {0}", request.ClientId);
                    }
                }
                else
                {
                    logger.LogError("No consent request matching id: {0}", id);
                }
            }
            else
            {
                logger.LogError("No id passed");
            }

            return null;
        }
    }
}
