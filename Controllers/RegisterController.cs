using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ftr_pt2.Models;
using Ftr_pt2.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ftr_pt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee.Access.ToLower() == "admin")
                {
                    var existingAdmin = await _context.Employees.FirstOrDefaultAsync(e => e.Email == employee.Email && e.Access.ToLower() == "admin");
                    if (existingAdmin != null)
                    {
                        return Conflict(new { error = "Admin with the same email already exists" });
                    }

                    // Additional logic for creating an admin, e.g., adding additional admin privileges
                }
                else if (employee.Access.ToLower() == "employee")
                {
                    var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == employee.Email && e.Access.ToLower() == "employee");
                    if (existingEmployee != null)
                    {
                        return Conflict(new { error = "Employee with the same email already exists" });
                    }

                    // Additional logic for creating an employee
                }
                else
                {
                    return BadRequest(new { error = "Invalid value for Access. Use 'admin' or 'employee'." });
                }

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
            }
            catch (DbUpdateException)
            {
                return Conflict(new { error = "Employee with the same email already exists" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
