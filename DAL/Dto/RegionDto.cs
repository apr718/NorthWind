using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("Region")]
    public class RegionDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int RegionID { get; set; }

        public string RegionDescription { get; set; }
    }
}
