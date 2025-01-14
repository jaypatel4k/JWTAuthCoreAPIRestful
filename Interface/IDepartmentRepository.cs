using JWTAuthCoreAPIRestful.Models;

namespace JWTAuthCoreAPIRestful.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentAsync();
        Task<Department> GetDepartmentByIdAsync(int deptid);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAync(Department department);
        Task DeleteDepartmentAsync(int deptid);
    }
}
