﻿using System;
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
            return _context.Leaderboard != null ?
                        View(await _context.Leaderboard.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.Leaderboard'  is null.");
        }

        // GET: Leaderboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leaderboard == null)
            {
                return NotFound();
            }

            var leaderboard = await _context.Leaderboard
                .FirstOrDefaultAsync(m => m.LeaderboardID == id);
            if (leaderboard == null)
            {
                return NotFound();
            }

            return View(leaderboard);
        }

        // GET: Leaderboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leaderboards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaderboardID,LeaderboardDate,TotalParticipants")] Leaderboard leaderboard)
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leaderboard == null)
            {
                return NotFound();
            }

            var leaderboard = await _context.Leaderboard.FindAsync(id);
            if (leaderboard == null)
            {
                return NotFound();
            }
            return View(leaderboard);
        }

        // POST: Leaderboards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaderboardID,LeaderboardDate,TotalParticipants")] Leaderboard leaderboard)
        {
            if (id != leaderboard.LeaderboardID)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leaderboard == null)
            {
                return NotFound();
            }

            var leaderboard = await _context.Leaderboard
                .FirstOrDefaultAsync(m => m.LeaderboardID == id);
            if (leaderboard == null)
            {
                return NotFound();
            }

            return View(leaderboard);
        }

        // POST: Leaderboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leaderboard == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.Leaderboard'  is null.");
            }
            var leaderboard = await _context.Leaderboard.FindAsync(id);
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
