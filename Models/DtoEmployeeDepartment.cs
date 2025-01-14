using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthCoreAPIRestful.Models
{
    public class DtoEmployeeDepartment
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }
    }
}
