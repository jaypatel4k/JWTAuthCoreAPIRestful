using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthCoreAPIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("GetDepartment")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartmentList()
        {
           var department = await _departmentRepository.GetAllDepartmentAsync();
            return Ok(department);
        }

        [HttpGet]
        [Route("{deptid:int}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int deptid)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(deptid);
            if(department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        [HttpPost]
        [Route("AddDepartment")]
        [Authorize]
        public async Task<ActionResult> AddDepartment([FromBody] Department department)
        {
            await _departmentRepository.AddDepartmentAsync(department);
            return CreatedAtAction("GetDepartmentById", new { department.DepartmentID }, department);
        }
        [HttpPut]
        [Route("UpdateDepartment/{deptid:int}")]
        [Authorize]
        public async Task<ActionResult>  UpdateDepartment(int deptid, [FromBody] Department department)
        {
            if(deptid != department.DepartmentID)
            {
                return BadRequest();
            }
            try
            {
                await _departmentRepository.UpdateDepartmentAync(department);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _departmentRepository.GetDepartmentByIdAsync(deptid) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteDepartment/{deptid:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteDepartment(int deptid)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(deptid);
            if(department == null)
            {
                return NotFound();
            }
            await _departmentRepository.DeleteDepartmentAsync(deptid);
            return NoContent();
        }

    }
}
