using FullStack_API.Data;
using FullStack_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext1;
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            this._fullStackDbContext1 = fullStackDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDbContext1.Employees?.ToListAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employees = await _fullStackDbContext1.Employees.Where(r=>r.id==id).FirstOrDefaultAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees([FromBody] Employee employeeRequest)
        {
            if (employeeRequest != null)
            {
                employeeRequest.id = Guid.NewGuid();
                await _fullStackDbContext1.Employees.AddAsync(employeeRequest);
                await _fullStackDbContext1.SaveChangesAsync();
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Deletemployees([FromRoute] Guid id)
        {
            if (id != null)
            {
                var employees = await _fullStackDbContext1.Employees.Where(r => r.id == id).FirstOrDefaultAsync();
                if(employees != null)
                {
                    _fullStackDbContext1.Employees.Remove(employees);
                    await _fullStackDbContext1.SaveChangesAsync();
                }
                return Ok();
            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployees([FromBody] Employee employeeRequest)
        {
            if (employeeRequest != null)
            {
                Employee data = await _fullStackDbContext1.Employees.Where(t => t.id == employeeRequest.id)?.FirstOrDefaultAsync();
                if (data != null)
                {
                    data.salary = employeeRequest.salary;
                    data.phone = employeeRequest.phone;
                    data.email = employeeRequest.email;
                    data.name = employeeRequest.name;
                    await _fullStackDbContext1.SaveChangesAsync();
                }


            }
            return Ok();
        }
    }
}
