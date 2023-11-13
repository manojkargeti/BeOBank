using Microsoft.AspNetCore.Mvc;

namespace BeOBank.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [Route("{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ViewData["StatusCode"] = statusCode;
            return View("HttpError");
        }
    }

}
