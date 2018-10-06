using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthWind.ViewModels
{
    public class InMemoryProductData
    {
        [Required]
        public int ProductId { get; set; }
        [Required, MaxLength(40)]
        public string ProductName { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Category> Categories { get; set; }
        [Required]
        public int? SelectedSupplierId { get; set; }
        [Required]
        public int? SelectedCategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        [Range(1, 5000)]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        [Required]
        public bool Discontinued { get; set; }
    }
}
