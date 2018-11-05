using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using NorthWind.Services.Interfaces;
using Services.Interfaces;

namespace NorthWind.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly IBLService _bLService;
        private readonly IConfigurationService _configurationService;

        public UploadFilesController(IBLService bLService, IConfigurationService configurationService)
        {
            _bLService = bLService;
            _configurationService = configurationService;
        }

        public IActionResult Index(int categoryId, string categoryName, string description)
        {
            var category = new Category
            {
                CategoryId = categoryId,
                CategoryName = categoryName,
                Description = description
            };
            return View(category);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadImage(IFormFile files, Category category)
        {
           
            using (var stream = new MemoryStream())
            {
                 await files.CopyToAsync(stream);
                category.Picture = stream.ToArray();
                await _bLService.AddOrUpdateCategoryAsync(category);
            }

            return RedirectToAction(nameof(Index), "Categories");
        }

    }

}