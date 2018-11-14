using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NorthWind.ViewComponents
{
    public class BreadcrumbsComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var path = Request.HttpContext.Request.Path;

            return View(path);
        }
    }
}
