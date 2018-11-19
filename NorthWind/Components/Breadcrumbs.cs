using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NorthWind.Components
{
    public class Breadcrumbs : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var path = Request.HttpContext.Request.Path;

            return View(path);
        }
    }
}
