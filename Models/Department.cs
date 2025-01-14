using System.ComponentModel.DataAnnotations;

namespace JWTAuthCoreAPIRestful.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

    }
}
