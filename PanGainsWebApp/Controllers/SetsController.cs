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
    public class SetsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public SetsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: Sets
        public async Task<IActionResult> Index()
        {
            return _context.Set != null ?
                        View(await _context.Set.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.Set'  is null.");
        }

        // GET: Sets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Set == null)
            {
                return NotFound();
            }

            var @set = await _context.Set
                .FirstOrDefaultAsync(m => m.SetID == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // GET: Sets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SetID,YourExerciseID,SetRow,SetType,Previous,Kg,Reps")] Set @set)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@set);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@set);
        }

        // GET: Sets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Set == null)
            {
                return NotFound();
            }

            var @set = await _context.Set.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }
            return View(@set);
        }

        // POST: Sets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SetID,YourExerciseID,SetRow,SetType,Previous,Kg,Reps")] Set @set)
        {
            if (id != @set.SetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@set);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetExists(@set.SetID))
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
            return View(@set);
        }

        // GET: Sets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Set == null)
            {
                return NotFound();
            }

            var @set = await _context.Set
                .FirstOrDefaultAsync(m => m.SetID == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // POST: Sets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Set == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.Set'  is null.");
            }
            var @set = await _context.Set.FindAsync(id);
            if (@set != null)
            {
                _context.Set.Remove(@set);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetExists(int id)
        {
            return (_context.Set?.Any(e => e.SetID == id)).GetValueOrDefault();
        }
    }
}
