using DAL.Dto;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {

        }

        public DbSet<CategoryDto> CategoriesCollection { get; set; }

        public DbSet<CustomerCustomerDemoDto> CustomerCustomerDemoCollection { get; set; }

        public DbSet<CustomerDemographicsDto> CustomerDemographicsCollection { get; set; }

        public DbSet<CustomerDto> CustomersCollection{ get; set; }

        public DbSet<EmployeeDto> EmployeesCollection { get; set; }

        public DbSet<EmployeeTerritoriesDto> EmployeeTerritoriesCollection{ get; set; }

        public DbSet<OrderDetailDto> OrderDetailsCollection{ get; set; }

        public DbSet<OrderDto> OrdersCollection { get; set; }

        public DbSet<ProductDto> ProductsCollection { get; set; }

        public DbSet<RegionDto> RegionsCollection { get; set; }

        public DbSet<ShipperDto> ShippersCollection { get; set; }

        public DbSet<SupplierDto> SuppliersCollection { get; set; }

        public DbSet<TerritoryDto> TerritoriesCollection { get; set; }
    }
}
