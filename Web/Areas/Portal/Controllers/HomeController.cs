using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Portal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
