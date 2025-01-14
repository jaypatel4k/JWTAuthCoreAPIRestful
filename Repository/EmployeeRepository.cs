using JWTAuthCoreAPIRestful.Data;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace JWTAuthCoreAPIRestful.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly JWTAuthCRUDContext _dbcontext;
        public EmployeeRepository(JWTAuthCRUDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _dbcontext.Employees.AddAsync(employee);
            await _dbcontext.SaveChangesAsync();

        }

        public async Task DeleteEmployeeAsync(int empid)
        {
            var employee = await _dbcontext.Employees.FindAsync(empid);
            if (employee != null)
            {
                _dbcontext.Employees.Remove(employee);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            return await _dbcontext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<DtoEmployeeDepartment>> GetAllEmployeeWithDepartmentAsync()
        {

            var emplyee = await _dbcontext.Employees.Join(
                    _dbcontext.Departments,
                    emp => emp.DepartmentID,
                    dept => dept.DepartmentID,
                    (emp, dept) => new DtoEmployeeDepartment
                    {
                        EmployeeID = emp.EmployeeID,
                        Name = emp.Name,
                        Age = emp.Age,
                        Gender = emp.Gender,
                        DepartmentID = emp.DepartmentID,
                        DepartmentName = dept.DepartmentName
                    }).ToListAsync();

            return emplyee;

        }

        public async Task<Employee> GetEmployeeByIdAsync(int empid)
        {
            return await _dbcontext.Employees.FindAsync(empid);
        }

        public async Task UpdateEmployeeAync(Employee employee)
        {
            _dbcontext.Update(employee);
            await _dbcontext.SaveChangesAsync();
        }
    }

}
