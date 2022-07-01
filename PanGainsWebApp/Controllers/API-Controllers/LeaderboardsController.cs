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
    public class LeaderboardsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public LeaderboardsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Leaderboards
        [HttpGet]
        public async Task<ActionResult<Leaderboard>> GetLeaderboard()
        {
            IEnumerable<Leaderboard> leaderboardsList = await _context.Leaderboard.ToListAsync();
            Leaderboard leaderboard = leaderboardsList.Where(l => l.LeaderboardDate.Month == DateTime.Now.Month && l.LeaderboardDate.Year == DateTime.Now.Year).First();

            if (leaderboard == null) return NotFound();

            return leaderboard;
        }

        // GET: api/Leaderboards/5
        [HttpGet("{leaderboardDate}")]
        public async Task<ActionResult<Leaderboard>> GetLeaderboard(DateTime leaderboardDate) // use the other one bro ^
        {
            IEnumerable<Leaderboard> leaderboardsList = await _context.Leaderboard.ToListAsync();
            Leaderboard leaderboard = leaderboardsList.Where(l => l.LeaderboardDate.Month == leaderboardDate.Month && l.LeaderboardDate.Year == leaderboardDate.Year).First();

            if (leaderboard == null) return NotFound();
            
            return leaderboard;
        }

        // PUT: api/Leaderboards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaderboard(int id, Leaderboard leaderboard)
        {
            if (id != leaderboard.LeaderboardID) return BadRequest();

            _context.Entry(leaderboard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaderboardExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Leaderboards
        [HttpPost]
        public async Task<ActionResult<Leaderboard>> PostLeaderboard(Leaderboard leaderboard)
        {
            _context.Leaderboard.Add(leaderboard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaderboard", new { id = leaderboard.LeaderboardID }, leaderboard);
        }

        // DELETE: api/Leaderboards/5
        [HttpDelete("{leaderboardID}")]
        public async Task<IActionResult> DeleteLeaderboard(int leaderboardID)
        {
            //delete ChallengeStats
            IEnumerable<ChallengeStats> challengeStatsList = await _context.ChallengeStats.ToListAsync();
            List<ChallengeStats> challengeStats = challengeStatsList.Where(c => c.LeaderboardID == leaderboardID).ToList();
            if (challengeStats.Any()) foreach (ChallengeStats c in challengeStats) _context.ChallengeStats.Remove(c);

            //remove Leaderboard
            Leaderboard leaderboard = await _context.Leaderboard.FindAsync(leaderboardID);
            if (leaderboard == null) return NotFound();
            _context.Leaderboard.Remove(leaderboard);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaderboardExists(int id)
        {
            return _context.Leaderboard.Any(e => e.LeaderboardID == id);
        }
    }
}
