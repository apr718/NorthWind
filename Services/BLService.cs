using DAL.Repositories.Interfaces;
using Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class BLService : IBLService
    {
        public readonly INorthwindRepository _northwindRepository;

        public BLService(INorthwindRepository northwindRepository)
        {
            _northwindRepository = northwindRepository;

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int pageSize)
        {
            return await _northwindRepository.GetAllProductsAsync(pageSize);
        }

        public async Task AddOrUpdateProductAsync(Product product)
        {
            await _northwindRepository.AddOrUpdateProductAsync(product);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _northwindRepository.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _northwindRepository.GetAllSuppliersAsync();
        }

        public async Task<Product> GetProductDetailsAsync(int Id)
        {
            return await _northwindRepository.GetProductDetailsAsync(Id);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _northwindRepository.DeleteProductAsync(id);
        }

        public bool ProductExist(int id)
        {
            return _northwindRepository.ProductExist(id);
        }
    }
}
