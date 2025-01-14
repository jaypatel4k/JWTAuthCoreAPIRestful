using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models;
using JWTAuthCoreAPIRestful.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthCoreAPIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("GetEmployee")]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeList()
        {
            var employee = await _employeeRepository.GetAllEmployeeAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{empid:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int empid)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empid);
            if( employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        [Route("AddEmployee")]
        [Authorize]
        public async Task<ActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
            return CreatedAtAction("GetEmployeeById", new { employee.EmployeeID }, employee);
        }
        [HttpPut]
        [Route("UpdateEmployee/{empid:int}")]
        [Authorize]
        public async Task<ActionResult> UpdateEmployee(int empid, [FromBody] Employee employee)
        {
            if (empid != employee.EmployeeID)
            {
                return BadRequest();
            }
            try
            {
                await _employeeRepository.UpdateEmployeeAync(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _employeeRepository.GetEmployeeByIdAsync(empid) == null)
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
        [Route("DeleteEmployee/{empid:int}")]
        [Authorize]
        public async Task<ActionResult> DeleteEmployee(int empid)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empid);
            if (employee == null)
            {
                return NotFound();
            }
            await _employeeRepository.DeleteEmployeeAsync(empid);
            return NoContent();
        }

        //[Route("SaveFile")]
        //[HttpPost]
        //public JsonResult SaveFile()
        //{
        //    try
        //    {
        //        var httpRequest = Request.Form;
        //        var postedFile = httpRequest.Files[0];
        //        string filename = postedFile.FileName;
        //        var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

        //        using (var stream = new FileStream(physicalPath, FileMode.Create))
        //        {
        //            stream.CopyTo(stream);
        //        }

        //        return new JsonResult(filename);
        //    }
        //    catch (Exception)
        //    {
        //        return new JsonResult("anonymous.png");
        //    }
        //}

        [HttpGet]
        [Route("GetAllDepartmentNames")]
        public async Task<IActionResult> GetAllDepartmentNames()
        {
            return Ok(await _departmentRepository.GetAllDepartmentAsync());
        }

        [HttpGet]
        [Route("GetAllEmployeeWithDepartment")]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeDepartmentList()
        {
            var employee = await _employeeRepository.GetAllEmployeeWithDepartmentAsync();
            return Ok(employee);
        }
    }
}
