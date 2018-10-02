using NorthWind.Models;
using System.Collections.Generic;

namespace NorthWind.ViewModels
{
    public class InMemoryProductData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Category> Categories { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedCategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
