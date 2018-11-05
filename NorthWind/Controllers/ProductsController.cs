using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using NorthWind.Services.Interfaces;
using NorthWind.ViewModels;
using Services.Interfaces;

namespace NorthWind.Controllers
{
    public class ProductsController : Controller
    {
        
        private readonly IBLService _bLService;
        private readonly IConfigurationService _configurationService;

        public ProductsController(IBLService bLService, IConfigurationService configurationService)
        {
            _bLService = bLService;
            _configurationService = configurationService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            int pageSize = _configurationService.PageSize;
            var products = await _bLService.GetAllProductsAsync(pageSize);
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var product = await _bLService.GetProductDetailsAsync(id.Value);
                
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var cats = await _bLService.GetAllCategoriesAsync(0);
            var suppls = await _bLService.GetAllSuppliersAsync();

            var inMemory = new InMemoryProductData
            {
                Categories = new List<Category>(),
                Suppliers = new List<Supplier>()
            };

            inMemory.Categories = cats;
            inMemory.Suppliers = suppls;
            
            return View(inMemory);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued,SelectedCategoryId,SelectedSupplierId")] InMemoryProductData inProduct)
        {
            var product = new Product
            {
                CategoryID = inProduct.SelectedCategoryId,
                SupplierID = inProduct.SelectedSupplierId,
                ProductID = inProduct.ProductId,
                ProductName = inProduct.ProductName,
                Discontinued = inProduct.Discontinued,
                QuantityPerUnit = inProduct.QuantityPerUnit,
                ReorderLevel = inProduct.ReorderLevel,
                UnitPrice = inProduct.UnitPrice,
                UnitsInStock = inProduct.UnitsInStock,
                UnitsOnOrder = inProduct.UnitsOnOrder
            };

            if (ModelState.IsValid)
            {
                await _bLService.AddOrUpdateProductAsync(product);

                return RedirectToAction(nameof(Index));
            }
            return View(inProduct);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bLService.GetProductDetailsAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            var vm = new InMemoryProductData {
                Categories = await _bLService.GetAllCategoriesAsync(0),
                Suppliers = await _bLService.GetAllSuppliersAsync(),
                SelectedCategoryId = product.CategoryID,
                SelectedSupplierId = product.SupplierID,
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                Discontinued = product.Discontinued,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder
            };

            return View(vm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,QuantityPerUnit,UnitPrice," +
            "UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued,SelectedCategoryId,SelectedSupplierId")] InMemoryProductData inMemoryProduct)
        {
            if (id != inMemoryProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = new Product
                    {
                        ProductID = inMemoryProduct.ProductId,
                        CategoryID = inMemoryProduct.SelectedCategoryId,
                        SupplierID = inMemoryProduct.SelectedSupplierId,
                        Discontinued = inMemoryProduct.Discontinued,
                        ProductName = inMemoryProduct.ProductName,
                        QuantityPerUnit = inMemoryProduct.QuantityPerUnit,
                        ReorderLevel = inMemoryProduct.ReorderLevel,
                        UnitPrice = inMemoryProduct.UnitPrice,
                        UnitsInStock = inMemoryProduct.UnitsInStock,
                        UnitsOnOrder = inMemoryProduct.UnitsOnOrder,
                    };

                    await _bLService.AddOrUpdateProductAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(inMemoryProduct.ProductId))
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
            return View(inMemoryProduct);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bLService.GetProductDetailsAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bLService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _bLService.ProductExist(id);
        }
    }
}
