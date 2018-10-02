using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Models;

namespace NorthWind.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "My application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "My contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
