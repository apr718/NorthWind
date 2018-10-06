using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface INorthwindRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync(int pageSize);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task AddOrUpdateProductAsync(Product product);
        Task<Product> GetProductDetailsAsync(int id);
        Task<Task> DeleteProductAsync(int id);
        bool ProductExist(int id);
    }
}
