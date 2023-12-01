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
    public class WorkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);

                if (employee == null)
                {
                    return NotFound(new { error = "Employee not found" });
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("employee/update/{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] Employee updatedEmployee)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);

                if (employee == null)
                {
                    return NotFound(new { error = "Employee not found" });
                }

                // Обновление данных сотрудника
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                // Добавьте остальные поля для обновления

                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Employee data updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("worktime/add")]
        public async Task<IActionResult> AddWorkTime([FromBody] WorkTime workTime)
        {
            try
            {
                // Проверка наличия сотрудника
                var employee = await _context.Employees.FindAsync(workTime.EmployeeId);
                if (employee == null)
                {
                    return NotFound(new { error = "Employee not found" });
                }

                // Добавление времени входа/выхода
                _context.WorkTimes.Add(workTime);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Work time added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
