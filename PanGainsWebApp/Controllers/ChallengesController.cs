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
    public class ChallengesController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public ChallengesController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: Challenges
        public async Task<IActionResult> Index()
        {
              return _context.Challenges != null ? 
                          View(await _context.Challenges.ToListAsync()) :
                          Problem("Entity set 'PanGainsWebAppContext.Challenges'  is null.");
        }

        // GET: Challenges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Challenges == null)
            {
                return NotFound();
            }

            var challenges = await _context.Challenges
                .FirstOrDefaultAsync(m => m.ChallengesID == id);
            if (challenges == null)
            {
                return NotFound();
            }

            return View(challenges);
        }

        // GET: Challenges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Challenges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChallengesID,ChallengeName")] Challenges challenges)
        {
            if (ModelState.IsValid)
            {
                _context.Add(challenges);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(challenges);
        }

        // GET: Challenges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Challenges == null)
            {
                return NotFound();
            }

            var challenges = await _context.Challenges.FindAsync(id);
            if (challenges == null)
            {
                return NotFound();
            }
            return View(challenges);
        }

        // POST: Challenges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChallengesID,ChallengeName")] Challenges challenges)
        {
            if (id != challenges.ChallengesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(challenges);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChallengesExists(challenges.ChallengesID))
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
            return View(challenges);
        }

        // GET: Challenges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Challenges == null)
            {
                return NotFound();
            }

            var challenges = await _context.Challenges
                .FirstOrDefaultAsync(m => m.ChallengesID == id);
            if (challenges == null)
            {
                return NotFound();
            }

            return View(challenges);
        }

        // POST: Challenges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Challenges == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.Challenges'  is null.");
            }
            var challenges = await _context.Challenges.FindAsync(id);
            if (challenges != null)
            {
                _context.Challenges.Remove(challenges);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChallengesExists(int id)
        {
          return (_context.Challenges?.Any(e => e.ChallengesID == id)).GetValueOrDefault();
        }
    }
}
