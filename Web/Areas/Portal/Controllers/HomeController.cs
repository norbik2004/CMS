using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;

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
