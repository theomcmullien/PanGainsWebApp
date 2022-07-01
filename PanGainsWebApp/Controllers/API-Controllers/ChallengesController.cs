using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public ChallengesController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Challenges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Challenges>>> GetChallenges()
        {
          if (_context.Challenges == null)
          {
              return NotFound();
          }
            return await _context.Challenges.ToListAsync();
        }

        // GET: api/Challenges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Challenges>> GetChallenges(int id)
        {
          if (_context.Challenges == null)
          {
              return NotFound();
          }
            var challenges = await _context.Challenges.FindAsync(id);

            if (challenges == null)
            {
                return NotFound();
            }

            return challenges;
        }

        // PUT: api/Challenges/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChallenges(int id, Challenges challenges)
        {
            if (id != challenges.ChallengesID)
            {
                return BadRequest();
            }

            _context.Entry(challenges).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengesExists(id))
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

        // POST: api/Challenges
        [HttpPost]
        public async Task<ActionResult<Challenges>> PostChallenges(Challenges challenges)
        {
          if (_context.Challenges == null)
          {
              return Problem("Entity set 'PanGainsAPIContext.Challenges'  is null.");
          }
            _context.Challenges.Add(challenges);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChallenges", new { id = challenges.ChallengesID }, challenges);
        }

        // DELETE: api/Challenges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallenges(int id)
        {
            if (_context.Challenges == null)
            {
                return NotFound();
            }
            var challenges = await _context.Challenges.FindAsync(id);
            if (challenges == null)
            {
                return NotFound();
            }

            _context.Challenges.Remove(challenges);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChallengesExists(int id)
        {
            return (_context.Challenges?.Any(e => e.ChallengesID == id)).GetValueOrDefault();
        }
    }
}
