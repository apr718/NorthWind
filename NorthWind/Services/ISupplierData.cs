using NorthWind.Models;
using System.Collections.Generic;

namespace NorthWind.Services
{
    interface ISupplierData
    {
        IEnumerable<Supplier> GetAll();
        Supplier Get(int id);
        //Supplier Add(Supplier restaurant);
    }
}
