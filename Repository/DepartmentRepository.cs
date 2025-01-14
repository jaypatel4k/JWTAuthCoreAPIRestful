using JWTAuthCoreAPIRestful.Data;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JWTAuthCoreAPIRestful.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly JWTAuthCRUDContext _dbcontext;
        public DepartmentRepository(JWTAuthCRUDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _dbcontext.Departments.AddAsync(department);
            await _dbcontext.SaveChangesAsync();

        }

        public async Task DeleteDepartmentAsync(int deptid)
        {
            var department =await _dbcontext.Departments.FindAsync(deptid);
            if(department != null)
            {
                _dbcontext.Departments.Remove(department);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentAsync()
        {
            return await _dbcontext.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int deptid)
        {
            return await _dbcontext.Departments.FindAsync(deptid);
        }

        public async Task UpdateDepartmentAync(Department department)
        {
            _dbcontext.Update(department);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
