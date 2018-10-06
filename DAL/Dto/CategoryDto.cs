using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("Categories")]
    public class CategoryDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }
    }
}
