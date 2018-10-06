using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBLService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(int pageSize);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task AddOrUpdateProductAsync(Product product);
        Task<Product> GetProductDetailsAsync(int id);
        Task DeleteProductAsync(int id);
        bool ProductExist(int id);
    }
}
