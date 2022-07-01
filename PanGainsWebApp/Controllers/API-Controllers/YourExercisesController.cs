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
    public class YourExercisesController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public YourExercisesController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/YourExercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YourExercise>>> GetYourExercise()
        {
            return await _context.YourExercise.ToListAsync();
        }

        // GET: api/YourExercises/5
        [HttpGet("{routineID}")]
        public async Task<ActionResult<IEnumerable<YourExercise>>> GetYourExercise(int routineID)
        {
            IEnumerable<YourExercise> yourExercisesList = await _context.YourExercise.ToListAsync();
            List<YourExercise> yourExercises = yourExercisesList.Where(y => y.RoutineID == routineID).ToList();

            if (yourExercises == null) return NotFound();

            return yourExercises;
        }

        // PUT: api/YourExercises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYourExercise(int id, YourExercise yourExercise)
        {
            if (id != yourExercise.YourExerciseID) return BadRequest();

            _context.Entry(yourExercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YourExerciseExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/YourExercises
        [HttpPost]
        public async Task<ActionResult<YourExercise>> PostYourExercise(YourExercise yourExercise)
        {
            _context.YourExercise.Add(yourExercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetYourExercise", new { id = yourExercise.YourExerciseID }, yourExercise);
        }

        // DELETE: api/YourExercises/5
        [HttpDelete("{yourExerciseID}")]
        public async Task<IActionResult> DeleteYourExercise(int yourExerciseID)
        {
            //remove Set
            IEnumerable<Set> setsList = await _context.Set.ToListAsync();
            List<Set> sets = setsList.Where(s => s.YourExerciseID == yourExerciseID).ToList();
            if (sets.Any()) foreach (Set s in sets) _context.Set.Remove(s);

            //remove YourExercise
            YourExercise yourExercise = await _context.YourExercise.FindAsync(yourExerciseID);
            if (yourExercise == null) return NotFound();
            _context.YourExercise.Remove(yourExercise);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YourExerciseExists(int id)
        {
            return _context.YourExercise.Any(e => e.YourExerciseID == id);
        }
    }
}
