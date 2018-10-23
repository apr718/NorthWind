using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBLService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(int pageSize);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageSize);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task AddOrUpdateProductAsync(Product product);
        Task<Product> GetProductDetailsAsync(int id);
        Task DeleteProductAsync(int id);
        bool ProductExist(int id);
        Task<Category> GetCategoryDetailsAsync(int id);
        Task AddOrUpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        bool CategoryExist(int id);
        byte[] CategryImage(int id);
    }
}
