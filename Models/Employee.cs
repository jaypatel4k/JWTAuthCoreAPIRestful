using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JWTAuthCoreAPIRestful.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public  string Name { get; set; }
        public int Age { get; set; }
        public  string Gender { get; set; }
        [ForeignKey("Department")]

        public int DepartmentID { get; set; }

        public Department Department { get; set; }

    }
}
