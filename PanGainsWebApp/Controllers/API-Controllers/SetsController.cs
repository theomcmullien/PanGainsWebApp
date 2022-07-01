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
    public class SetsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public SetsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Sets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Set>>> GetSet()
        {
            return await _context.Set.ToListAsync();
        }

        // GET: api/Sets/5
        [HttpGet("{yourExerciseID}")]
        public async Task<ActionResult<IEnumerable<Set>>> GetSet(int yourExerciseID)
        {
            IEnumerable<Set> setsList = await _context.Set.ToListAsync();
            List<Set> sets = setsList.Where(s => s.YourExerciseID == yourExerciseID).ToList();

            if (sets == null) return NotFound();

            return sets;
        }

        // PUT: api/Sets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSet(int id, Set set)
        {
            if (id != set.SetID) return BadRequest();

            _context.Entry(set).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Sets
        [HttpPost]
        public async Task<ActionResult<Set>> PostSet(Set set)
        {
            _context.Set.Add(set);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSet", new { id = set.SetID }, set);
        }

        // DELETE: api/Sets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSet(int id)
        {
            var set = await _context.Set.FindAsync(id);

            if (set == null) return NotFound();

            _context.Set.Remove(set);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetExists(int id)
        {
            return _context.Set.Any(e => e.SetID == id);
        }
    }
}
