using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("CustomerCustomerDemo")]
    public class CustomerCustomerDemoDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string CustomerID { get; set; }

        public string CustomerTypeID { get; set; }
    }
}
