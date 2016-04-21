using Microsoft.AspNet.Mvc;

namespace Bodegas.Auth.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
