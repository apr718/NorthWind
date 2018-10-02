using System;
using System.Collections.Generic;
using System.Linq;
using NorthWind.Data;
using NorthWind.Models;

namespace NorthWind.Services
{
    public class SqlProductData : IProductData
    {
        private NorthWindDbContext _context;

        public SqlProductData(NorthWindDbContext context)
        {
            _context = context;
        }

        public Product Get(int id)
        {
            return _context.Products.FirstOrDefault(r => r.ProductId == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.OrderBy(r => r.ProductName);
        }
    }
}
