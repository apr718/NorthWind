using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface INorthwindRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageSize);
        Task<IEnumerable<Product>> GetAllProductsAsync(int pageSize);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task AddOrUpdateProductAsync(Product product);
        Task<Product> GetProductDetailsAsync(int id);
        Task<Task> DeleteProductAsync(int id);
        bool ProductExist(int id);
        Task<Category> GetCategoryDetailsAsync(int id);
        Task AddOrUpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        bool CategoryExist(int id);
        byte[] GetCategoryImage(int id);
    }
}
