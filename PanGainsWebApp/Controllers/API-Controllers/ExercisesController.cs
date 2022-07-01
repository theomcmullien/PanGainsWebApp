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
    public class ExercisesController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public ExercisesController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercise()
        {
            return await _context.Exercise.ToListAsync();
        }

        // GET: api/Exercises/5
        [HttpGet("{exerciseID}")]
        public async Task<ActionResult<Exercise>> GetExercise(int exerciseID)
        {
            Exercise exercise = await _context.Exercise.FindAsync(exerciseID);

            if (exercise == null) return NotFound();

            return exercise;
        }

        // PUT: api/Exercises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(int id, Exercise exercise)
        {
            if (id != exercise.ExerciseID) return BadRequest();

            _context.Entry(exercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Exercises
        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(Exercise exercise)
        {
            _context.Exercise.Add(exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercise", new { id = exercise.ExerciseID }, exercise);
        }

        // DELETE: api/Exercises/5
        [HttpDelete("{exerciseID}")]
        public async Task<IActionResult> DeleteExercise(int exerciseID)
        {
            //remove YourExercise
            IEnumerable<YourExercise> yourExerciseslist = await _context.YourExercise.ToListAsync();
            List<YourExercise> yourExercises = yourExerciseslist.Where(y => y.ExerciseID == exerciseID).ToList();
            if (yourExercises.Any()) foreach (YourExercise y in yourExercises) _context.YourExercise.Remove(y);

            //remove Exercise
            Exercise exercise = await _context.Exercise.FindAsync(exerciseID);
            if (exercise == null) return NotFound();
            _context.Exercise.Remove(exercise);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercise.Any(e => e.ExerciseID == id);
        }
    }
}
