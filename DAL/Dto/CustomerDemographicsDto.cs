using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("CustomerDemographics")]
    public class CustomerDemographicsDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string CustomerTypeID { get; set; }

        public string CustomerDesc { get; set; }
    }
}
