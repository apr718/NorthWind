using DAL.Context;
using DAL.Dto;
using DAL.Mapper.Interfaces;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class NorthwindRepository : INorthwindRepository
    {
        private readonly NorthwindContext _context;
        private readonly IEntityBuilder _entityBuilder;

        public NorthwindRepository(NorthwindContext context, IEntityBuilder entityBuilder)
        {
            _context = context;
            _entityBuilder = entityBuilder;
        }

        public async Task AddOrUpdateProductAsync(Product product)
        {
            var productDto = _entityBuilder.Map<Product, ProductDto>(product);

            if (product.ProductID != default(int))
            {
                var previous = await _context.ProductsCollection.FirstAsync(c => c.ProductID == product.ProductID);
                previous.CategoryID = product.CategoryID;
                previous.Discontinued = product.Discontinued;
                previous.ProductName = product.ProductName;
                previous.QuantityPerUnit = product.QuantityPerUnit;
                previous.ReorderLevel = product.ReorderLevel;
                previous.SupplierID = product.SupplierID;
                previous.UnitPrice = product.UnitPrice;
                previous.UnitsInStock = product.UnitsInStock;
                previous.UnitsOnOrder = product.UnitsOnOrder;
            }
            else
            {
                 _context.ProductsCollection.Add(productDto);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = int.MaxValue;
            }
            var catsCollection = await _context.CategoriesCollection.Take(pageSize).ToListAsync();
            var result = catsCollection.Select(c => _entityBuilder.Map<CategoryDto, Category>(c));
            return result;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = int.MaxValue;
            }
            var joinedData = await _context.ProductsCollection
                //.Include(p => p.Category)
                //.Include(p => p.Supplier)
                .Join(_context.CategoriesCollection, x1 => x1.CategoryID, x2 => x2.CategoryId, (a, b) =>
                new
                {
                    Product = a,
                    Category = b
                })
                .Join(_context.SuppliersCollection, x1 => x1.Product.SupplierID, x2 => x2.SupplierID, (a, b) =>
                new
                {
                    Product = a.Product,
                    Category = a.Category,
                    Supplier = b
                })
                .Take(pageSize)
                .ToListAsync();

            return joinedData.Select(c => new
            {
                Product = _entityBuilder.Map<ProductDto, Product>(c.Product, (a, b) =>
                {
                    b.CategoryName = c.Category.CategoryName;
                    b.SupplierName = c.Supplier.CompanyName;
                })
            }).Select(c => c.Product);
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.SuppliersCollection.Select(c => _entityBuilder.Map<SupplierDto, Supplier>(c)).ToListAsync();
        }

        public async Task<Product> GetProductDetailsAsync(int id)
        {
            var pr = await _context.ProductsCollection.FirstOrDefaultAsync(m => m.ProductID == id);

            return _entityBuilder.Map<ProductDto, Product>(pr);
        }

        public async Task<Task> DeleteProductAsync(int id)
        {
            var productDto = new ProductDto
            {
                ProductID = id
            };
            _context.ProductsCollection.Remove(productDto);
            await _context.SaveChangesAsync();
            return null;
        }

        public bool ProductExist(int id)
        {
            var result =_context.ProductsCollection.Any(e => e.ProductID == id);
            return result;
        }

        public async Task<Category> GetCategoryDetailsAsync(int id)
        {
            var category = await _context.CategoriesCollection.FirstOrDefaultAsync(m => m.CategoryId == id);

            return _entityBuilder.Map<CategoryDto, Category>(category);
        }

        public async Task AddOrUpdateCategoryAsync(Category category)
        {
            var categoryDto = _entityBuilder.Map<Category, CategoryDto>(category);
            if (category.CategoryId != default(int))
            {
                await _context.CategoriesCollection.AddAsync(categoryDto);
            }
            else
            {
                _context.CategoriesCollection.Update(categoryDto);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var categoryDto = new CategoryDto
            {
                CategoryId = id
            };
            _context.CategoriesCollection.Remove(categoryDto);
            await _context.SaveChangesAsync();
        }

        public bool CategoryExist(int id)
        {
            var result = _context.ProductsCollection.Any(e => e.ProductID == id);
            return result;
        }
    }

}
