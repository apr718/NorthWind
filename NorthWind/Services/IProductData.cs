using NorthWind.Models;
using System.Collections.Generic;

namespace NorthWind.Services
{
    interface IProductData
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
       // Product Add(Product restaurant);
    }
}
