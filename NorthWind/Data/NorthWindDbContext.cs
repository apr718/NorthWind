using Microsoft.EntityFrameworkCore;
using NorthWind.Models;

namespace NorthWind.Data
{
    public class NorthWindDbContext : DbContext
    {
        public NorthWindDbContext(DbContextOptions options)
            :base(options)
        {
                
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
