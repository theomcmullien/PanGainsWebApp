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
    public class ChallengeStatsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public ChallengeStatsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: ChallengeStats
        public async Task<IActionResult> Index()
        {
            return _context.ChallengeStats != null ?
                        View(await _context.ChallengeStats.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.ChallengeStats'  is null.");
        }

        // GET: ChallengeStats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChallengeStats == null)
            {
                return NotFound();
            }

            var challengeStats = await _context.ChallengeStats
                .FirstOrDefaultAsync(m => m.ChallengeStatsID == id);
            if (challengeStats == null)
            {
                return NotFound();
            }

            return View(challengeStats);
        }

        // GET: ChallengeStats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChallengeStats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChallengeStatsID,AccountID,LeaderboardID,ChallengeTotalReps")] ChallengeStats challengeStats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(challengeStats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(challengeStats);
        }

        // GET: ChallengeStats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChallengeStats == null)
            {
                return NotFound();
            }

            var challengeStats = await _context.ChallengeStats.FindAsync(id);
            if (challengeStats == null)
            {
                return NotFound();
            }
            return View(challengeStats);
        }

        // POST: ChallengeStats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChallengeStatsID,AccountID,LeaderboardID,ChallengeTotalReps")] ChallengeStats challengeStats)
        {
            if (id != challengeStats.ChallengeStatsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(challengeStats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChallengeStatsExists(challengeStats.ChallengeStatsID))
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
            return View(challengeStats);
        }

        // GET: ChallengeStats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChallengeStats == null)
            {
                return NotFound();
            }

            var challengeStats = await _context.ChallengeStats
                .FirstOrDefaultAsync(m => m.ChallengeStatsID == id);
            if (challengeStats == null)
            {
                return NotFound();
            }

            return View(challengeStats);
        }

        // POST: ChallengeStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChallengeStats == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.ChallengeStats'  is null.");
            }
            var challengeStats = await _context.ChallengeStats.FindAsync(id);
            if (challengeStats != null)
            {
                _context.ChallengeStats.Remove(challengeStats);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChallengeStatsExists(int id)
        {
            return (_context.ChallengeStats?.Any(e => e.ChallengeStatsID == id)).GetValueOrDefault();
        }
    }
}
