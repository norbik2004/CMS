using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Portal.Controllers
{
    [Area("Portal")]
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
