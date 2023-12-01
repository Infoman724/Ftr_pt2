using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ftr_pt2.Models;
using Ftr_pt2.Data;
using System.Threading.Tasks;

namespace Ftr_pt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] EmployeeLoginCredentials loginCredentials)
        {
            try
            {
                var employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Email == loginCredentials.Email && e.Password == loginCredentials.Password);

                if (employee == null)
                {
                    return NotFound(new { error = "Invalid email or password" });
                }

                // Дополнительная логика, если нужно

                // Возвращаем информацию о сотруднике
                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    employeeId = employee.EmployeeId,
                    firstName = employee.FirstName,
                    lastName = employee.LastName,
                    // Другие поля, которые вы хотите вернуть
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
