using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("Order Details")]
    public class OrderDetailDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }
    }
}
