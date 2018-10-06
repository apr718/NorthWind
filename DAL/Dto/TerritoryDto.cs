using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("Territories")]
    public class TerritoryDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
    }
}
