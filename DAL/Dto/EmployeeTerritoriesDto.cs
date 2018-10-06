using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Dto
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritoriesDto
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int EmployeeID { get; set; }

        public int TerritoryID { get; set; }
    }
}
