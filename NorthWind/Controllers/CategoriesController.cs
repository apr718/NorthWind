using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using NorthWind.Extensions;
using NorthWind.Services.Interfaces;
using Services.Interfaces;

namespace NorthWind.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IBLService _bLService;
        private readonly IConfigurationService _configurationService;

        public CategoriesController(IBLService bLService, IConfigurationService configurationService)
        {
            _bLService = bLService;
            _configurationService = configurationService;
        }

        // GET: Categories
        [SampleActionFilter]
        public async Task<IActionResult> Index()
        {
            int pageSize = _configurationService.PageSize;
            var categories = await _bLService.GetAllCategoriesAsync(pageSize);
            return View(categories);
        }

        // GET: Categories/Details/5
        [SampleActionFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var category = await _bLService.GetCategoryDetailsAsync(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [SampleActionFilter]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _bLService.AddOrUpdateCategoryAsync(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [SampleActionFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _bLService.GetCategoryDetailsAsync(id.Value);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [SampleActionFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,CategoryName,Description")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bLService.AddOrUpdateCategoryAsync(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _bLService.GetCategoryDetailsAsync(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [SampleActionFilter]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             await _bLService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _bLService.CategoryExist(id);
        }

        public ActionResult RenderImage(int categoryId)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] image = _bLService.CategryImage(categoryId);

                if (categoryId == 2)
                {
                    ms.Write(image);
                }
                else
                {
                    ms.Write(image, 78, image.Length - 78);
                }
                

                return File(ms.ToArray(), "image/jpeg");
            }

        }

    }
}
