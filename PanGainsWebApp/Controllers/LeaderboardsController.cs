using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Controllers
{
    public class LeaderboardsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public LeaderboardsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: Leaderboards
        public async Task<IActionResult> Index()
        {
            var accountsList = await _context.Account.ToListAsync();
            var leaderboardsList = await _context.Leaderboard.ToListAsync();
            var challengeStatsList = await _context.ChallengeStats.ToListAsync();
            var challengesList = await _context.Challenges.ToListAsync();

            LeaderboardDetails leaderboardDetails = new LeaderboardDetails();


            //List<LeaderboardPosition> LeaderboardPositions
            if (leaderboardsList.Count > 0)
            {
                List<LeaderboardPosition> leaderboardPositions = new List<LeaderboardPosition>();

                int leaderboardID = leaderboardsList.Where(l => l.LeaderboardDate.Month == DateTime.Now.Month && l.LeaderboardDate.Year == DateTime.Now.Year).Select(l => l.LeaderboardID).First();

                foreach (ChallengeStats c in challengeStatsList)
                {
                    if (c.LeaderboardID == leaderboardID)
                    {
                        Account a = accountsList.Where(a => a.AccountID == c.AccountID).First();
                        leaderboardPositions.Add(new LeaderboardPosition(a.Firstname, a.Lastname, c.ChallengeTotalReps));
                    }
                }

                leaderboardPositions.Sort();
                leaderboardPositions.Reverse();
                leaderboardDetails.LeaderboardPositions = leaderboardPositions;
            }

            Leaderboard maxLeaderboard = new Leaderboard();
            maxLeaderboard.LeaderboardID = 0;
            foreach (Leaderboard l in leaderboardsList)
            {
                if (l.LeaderboardID > maxLeaderboard.LeaderboardID) maxLeaderboard = l;

                //string CurrentChallengeName
                if (l.LeaderboardDate.Month == DateTime.Now.Month && l.LeaderboardDate.Year == DateTime.Now.Year)
                {
                    leaderboardDetails.CurrentChallengeName = challengesList.Where(c => c.ChallengesID == l.ChallengesID).Select(c => c.ChallengeName).First();
                }
            }

            //int LeaderboardID
            DateTime nextMonthNow = DateTime.Now.AddMonths(1);
            if (maxLeaderboard.LeaderboardDate.Month == nextMonthNow.Month)
            {
                leaderboardDetails.LeaderboardID = maxLeaderboard.LeaderboardID;

                //string NextChallengeName
                leaderboardDetails.NextChallengeName = challengesList.Where(c => c.ChallengesID == maxLeaderboard.ChallengesID).Select(c => c.ChallengeName).First();
            }
            else leaderboardDetails.LeaderboardID = 0;



            return View(leaderboardDetails);
        }

        // GET: Leaderboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leaderboard == null) return NotFound();

            var leaderboard = await _context.Leaderboard.FirstOrDefaultAsync(m => m.LeaderboardID == id);

            if (leaderboard == null) return NotFound();

            return View(leaderboard);
        }

        // GET: Leaderboards/Create
        public async Task<IActionResult> Create()
        {
            var leaderboardsList = await _context.Leaderboard.ToListAsync();
            
            CreateLeaderboard model = new CreateLeaderboard();

            int maxLeaderboardID = 0;
            foreach (Leaderboard l in leaderboardsList) if (l.LeaderboardID > maxLeaderboardID) maxLeaderboardID = l.LeaderboardID;
            maxLeaderboardID++;
            model.LeaderboardID = maxLeaderboardID;

            model.Challenges = await _context.Challenges.ToListAsync();

            return View(model);
        }

        // POST: Leaderboards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaderboardID,ChallengesID,LeaderboardDate,TotalParticipants")] Leaderboard leaderboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaderboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaderboard);
        }

        // GET: Leaderboards/Edit/5
        public async Task<IActionResult> Edit(int? leaderboardID)
        {
            if (leaderboardID == null || _context.Leaderboard == null)
            {
                return NotFound();
            }

            var leaderboard = await _context.Leaderboard.FindAsync(leaderboardID);
            if (leaderboard == null)
            {
                return NotFound();
            }
            return View(leaderboard);
        }

        // POST: Leaderboards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int leaderboardID, [Bind("LeaderboardID,ChallengesID,LeaderboardDate,TotalParticipants")] Leaderboard leaderboard)
        {
            if (leaderboardID != leaderboard.LeaderboardID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaderboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaderboardExists(leaderboard.LeaderboardID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaderboard);
        }

        // GET: Leaderboards/Delete/5
        public async Task<IActionResult> Delete(int? leaderboardID)
        {
            if (leaderboardID == null || _context.Leaderboard == null) return NotFound();

            var leaderboard = await _context.Leaderboard.FirstOrDefaultAsync(m => m.LeaderboardID == leaderboardID);

            if (leaderboard == null) return NotFound();

            return View(leaderboard);
        }

        // POST: Leaderboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int leaderboardID)
        {
            if (_context.Leaderboard == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.Leaderboard'  is null.");
            }
            var leaderboard = await _context.Leaderboard.FindAsync(leaderboardID);
            if (leaderboard != null)
            {
                _context.Leaderboard.Remove(leaderboard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaderboardExists(int id)
        {
          return (_context.Leaderboard?.Any(e => e.LeaderboardID == id)).GetValueOrDefault();
        }
    }
}
