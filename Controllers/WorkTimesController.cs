using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ftr_pt2.Models;
using Ftr_pt2.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ftr_pt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTimesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkTimesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/worktimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTime>>> GetWorkTimes()
        {
            return await _context.WorkTimes.ToListAsync();
        }

        // GET: api/worktimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTime>> GetWorkTime(int id)
        {
            var workTime = await _context.WorkTimes.FindAsync(id);

            if (workTime == null)
            {
                return NotFound();
            }

            return workTime;
        }

        // POST: api/worktimes
        [HttpPost]
        public async Task<ActionResult<WorkTime>> CreateWorkTime(WorkTime workTime)
        {
            _context.WorkTimes.Add(workTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateWorkTime", new { id = workTime.WorkTimeId }, workTime);
        }

        // PUT: api/worktimes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkTime(int id, WorkTime workTime)
        {
            if (id != workTime.WorkTimeId)
            {
                return BadRequest();
            }

            _context.Entry(workTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTimeExists(id))
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

        // DELETE: api/worktimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTime(int id)
        {
            var workTime = await _context.WorkTimes.FindAsync(id);
            if (workTime == null)
            {
                return NotFound();
            }

            _context.WorkTimes.Remove(workTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkTimeExists(int id)
        {
            return _context.WorkTimes.Any(e => e.WorkTimeId == id);
        }
    }
}
