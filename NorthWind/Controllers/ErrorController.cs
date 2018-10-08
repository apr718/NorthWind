using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class ErrorController : Controller
    {

        [AllowAnonymous]
        public IActionResult Error()
        {
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = error?.GetBaseException()?.Message ?? "none" });
        }
    }
}