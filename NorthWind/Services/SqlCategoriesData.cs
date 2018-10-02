using System;
using System.Collections.Generic;
using System.Linq;
using NorthWind.Data;
using NorthWind.Models;

namespace NorthWind.Services
{
    public class SqlCategoriesData : ICategoryData
    {
        private NorthWindDbContext _context;

        public SqlCategoriesData(NorthWindDbContext context)
        {
            _context = context;
        }

        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Get(int id)
        {
            return _context.Categories.FirstOrDefault(r => r.CategoryID == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.OrderBy(r=>r.CategoryName);
        }
    }
}
