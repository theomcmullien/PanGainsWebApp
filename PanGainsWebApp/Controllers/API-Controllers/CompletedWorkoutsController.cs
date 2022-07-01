#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompletedWorkoutsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public CompletedWorkoutsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/CompletedWorkouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedWorkout>>> GetCompletedWorkout()
        {
            return await _context.CompletedWorkout.ToListAsync();
        }

        // GET: api/CompletedWorkouts/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<IEnumerable<CompletedWorkout>>> GetCompletedWorkout(int accountID)
        {
            IEnumerable<CompletedWorkout> completedWorkoutsList = await _context.CompletedWorkout.ToListAsync();
            List<CompletedWorkout> completedWorkouts = completedWorkoutsList.Where(c => c.AccountID == accountID).ToList();

            if (completedWorkouts == null) return NotFound();

            return completedWorkouts;
        }

        // PUT: api/CompletedWorkouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompletedWorkout(int id, CompletedWorkout completedWorkout)
        {
            if (id != completedWorkout.CompletedWorkoutID) return BadRequest();

            _context.Entry(completedWorkout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompletedWorkoutExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/CompletedWorkouts
        [HttpPost]
        public async Task<ActionResult<CompletedWorkout>> PostCompletedWorkout(CompletedWorkout completedWorkout)
        {
            _context.CompletedWorkout.Add(completedWorkout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompletedWorkout", new { id = completedWorkout.CompletedWorkoutID }, completedWorkout);
        }

        // DELETE: api/CompletedWorkouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompletedWorkout(int id)
        {
            var completedWorkout = await _context.CompletedWorkout.FindAsync(id);

            if (completedWorkout == null) return NotFound();

            _context.CompletedWorkout.Remove(completedWorkout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompletedWorkoutExists(int id)
        {
            return _context.CompletedWorkout.Any(e => e.CompletedWorkoutID == id);
        }
    }
}
