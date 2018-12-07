using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Service.Interfaces;
using Services.Interfaces;

namespace NorthWind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly IBLService _bLService;
        private readonly IConfigurationService _configurationService;

        public CategoriesController(IBLService bLService, IConfigurationService configurationService)
        {
            _bLService = bLService;
            _configurationService = configurationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int pageSize = _configurationService.PageSize;
            var categories = await _bLService.GetAllCategoriesAsync(pageSize);
            return Ok(categories);
        }
    }
}
