using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("Shippers")]
    public class ShipperDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
