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
    public class RoutinesController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public RoutinesController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: Routines
        public async Task<IActionResult> Index()
        {
            return _context.Routine != null ?
                        View(await _context.Routine.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.Routine'  is null.");
        }

        // GET: Routines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Routine == null)
            {
                return NotFound();
            }

            var routine = await _context.Routine
                .FirstOrDefaultAsync(m => m.RoutineID == id);
            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        // GET: Routines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoutineID,FolderID,RoutineName")] Routine routine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routine);
        }

        // GET: Routines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Routine == null)
            {
                return NotFound();
            }

            var routine = await _context.Routine.FindAsync(id);
            if (routine == null)
            {
                return NotFound();
            }
            return View(routine);
        }

        // POST: Routines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoutineID,FolderID,RoutineName")] Routine routine)
        {
            if (id != routine.RoutineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutineExists(routine.RoutineID))
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
            return View(routine);
        }

        // GET: Routines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Routine == null)
            {
                return NotFound();
            }

            var routine = await _context.Routine
                .FirstOrDefaultAsync(m => m.RoutineID == id);
            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        // POST: Routines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Routine == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.Routine'  is null.");
            }
            var routine = await _context.Routine.FindAsync(id);
            if (routine != null)
            {
                _context.Routine.Remove(routine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutineExists(int id)
        {
            return (_context.Routine?.Any(e => e.RoutineID == id)).GetValueOrDefault();
        }
    }
}
