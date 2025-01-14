using JWTAuthCoreAPIRestful.Models;

namespace JWTAuthCoreAPIRestful.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeByIdAsync(int deptid);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAync(Employee employee);
        Task DeleteEmployeeAsync(int empid);

        Task<IEnumerable<DtoEmployeeDepartment>> GetAllEmployeeWithDepartmentAsync();
    }
}
