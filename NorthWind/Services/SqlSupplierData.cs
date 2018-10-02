using System.Collections.Generic;
using System.Linq;
using NorthWind.Data;
using NorthWind.Models;

namespace NorthWind.Services
{
    public class SqlSupplierData: ISupplierData
    {
        private NorthWindDbContext _context;
        public SqlSupplierData(NorthWindDbContext context)
        {
            _context = context;
        }

        public Supplier Get(int id)
        {
            return _context.Suppliers.FirstOrDefault(r => r.SupplierID == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers.OrderBy(r => r.CompanyName);
        }
    }
}
