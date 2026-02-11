using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Error.Controllers
{
    [Area("Error")]
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("{code:int?}")]
        public IActionResult Index(int? code = null)
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;

            if (exception is DomainException domainEx)
            {
                code = domainEx.StatusCode;
            }

            code ??= 500;

            return code switch
            {
                403 => View("Forbidden"),
                404 => View("NotFound"),
                400 => View("BadRequest"),
                _ => View("General"),
            };
        }
    }
}
