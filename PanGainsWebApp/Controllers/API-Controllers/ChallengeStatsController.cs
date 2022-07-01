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
    public class ChallengeStatsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public ChallengeStatsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/ChallengeStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChallengeStats>>> GetChallengeStats()
        {
            IEnumerable<ChallengeStats> challengeStatsList = await _context.ChallengeStats.ToListAsync();
            IEnumerable<Leaderboard> leaderboardsList = await _context.Leaderboard.ToListAsync();

            Leaderboard leaderboard = leaderboardsList.Where(l => l.LeaderboardDate.Month == DateTime.Now.Month && l.LeaderboardDate.Year == DateTime.Now.Year).First();

            var challengeStats = challengeStatsList.Where(c => c.LeaderboardID == leaderboard.LeaderboardID).ToList();

            if (challengeStats == null) return NotFound();

            return challengeStats;
        }

        // GET: api/ChallengeStats/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<IEnumerable<ChallengeStats>>> GetChallengeStats(int accountID)
        {
            IEnumerable<ChallengeStats> challengeStatsList = await _context.ChallengeStats.ToListAsync();
            ChallengeStats[] challengeStats = challengeStatsList.Where(c => c.AccountID == accountID).ToArray();

            if (challengeStats == null) return NotFound();

            return challengeStats;
        }

        // PUT: api/ChallengeStats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChallengeStats(int id, ChallengeStats challengeStats)
        {
            if (id != challengeStats.ChallengeStatsID) return BadRequest();

            _context.Entry(challengeStats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeStatsExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/ChallengeStats
        [HttpPost]
        public async Task<ActionResult<ChallengeStats>> PostChallengeStats(ChallengeStats challengeStats)
        {
            //foreach (Leaderboard leaderboard in await _context.Leaderboard.ToListAsync())
            //{
            //    if (leaderboard.LeaderboardDate.Month == DateTime.Now.Month && leaderboard.LeaderboardDate.Year == DateTime.Now.Year)
            //    {
            //        challengeStats.LeaderboardID = leaderboard.LeaderboardID;
            //    }
            //}

            _context.ChallengeStats.Add(challengeStats);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChallengeStats", new { id = challengeStats.ChallengeStatsID }, challengeStats);
        }

        // DELETE: api/ChallengeStats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallengeStats(int id)
        {
            ChallengeStats challengeStats = await _context.ChallengeStats.FindAsync(id);
            
            if (challengeStats == null) return NotFound();

            _context.ChallengeStats.Remove(challengeStats);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChallengeStatsExists(int id)
        {
            return _context.ChallengeStats.Any(e => e.ChallengeStatsID == id);
        }
    }
}
