using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthWind.Data;
using NorthWind.Models;
using NorthWind.ViewModels;

namespace NorthWind.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthWindDbContext _context;

        public ProductsController(NorthWindDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var lst = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
            return View(lst);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var vm = new InMemoryProductData {
                Categories = _context.Categories.ToList(),
                Suppliers = _context.Suppliers.ToList(),
                SelectedCategoryId = product.Category.CategoryID,
                SelectedSupplierId = product.Supplier.SupplierID,
                ProductId = product.ProductId,
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
                        ProductId = inMemoryProduct.ProductId,
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
                    _context.Update(product);
                    await _context.SaveChangesAsync();
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
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
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
