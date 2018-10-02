using NorthWind.Models;
using System.Collections.Generic;

namespace NorthWind.Services
{
    interface ICategoryData
    {
        IEnumerable<Category> GetAll();
        Category Get(int id);
        Category Add(Category restaurant);
    }
}
