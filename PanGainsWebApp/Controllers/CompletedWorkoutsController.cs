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
    public class CompletedWorkoutsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public CompletedWorkoutsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: CompletedWorkouts
        public async Task<IActionResult> Index()
        {
            return _context.CompletedWorkout != null ?
                        View(await _context.CompletedWorkout.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.CompletedWorkout'  is null.");
        }

        // GET: CompletedWorkouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompletedWorkout == null)
            {
                return NotFound();
            }

            var completedWorkout = await _context.CompletedWorkout
                .FirstOrDefaultAsync(m => m.CompletedWorkoutID == id);
            if (completedWorkout == null)
            {
                return NotFound();
            }

            return View(completedWorkout);
        }

        // GET: CompletedWorkouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompletedWorkouts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompletedWorkoutID,AccountID,RoutineID,DateCompleted,Duration,TotalWeightLifted")] CompletedWorkout completedWorkout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(completedWorkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(completedWorkout);
        }

        // GET: CompletedWorkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompletedWorkout == null)
            {
                return NotFound();
            }

            var completedWorkout = await _context.CompletedWorkout.FindAsync(id);
            if (completedWorkout == null)
            {
                return NotFound();
            }
            return View(completedWorkout);
        }

        // POST: CompletedWorkouts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompletedWorkoutID,AccountID,RoutineID,DateCompleted,Duration,TotalWeightLifted")] CompletedWorkout completedWorkout)
        {
            if (id != completedWorkout.CompletedWorkoutID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(completedWorkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompletedWorkoutExists(completedWorkout.CompletedWorkoutID))
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
            return View(completedWorkout);
        }

        // GET: CompletedWorkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompletedWorkout == null)
            {
                return NotFound();
            }

            var completedWorkout = await _context.CompletedWorkout
                .FirstOrDefaultAsync(m => m.CompletedWorkoutID == id);
            if (completedWorkout == null)
            {
                return NotFound();
            }

            return View(completedWorkout);
        }

        // POST: CompletedWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompletedWorkout == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.CompletedWorkout'  is null.");
            }
            var completedWorkout = await _context.CompletedWorkout.FindAsync(id);
            if (completedWorkout != null)
            {
                _context.CompletedWorkout.Remove(completedWorkout);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompletedWorkoutExists(int id)
        {
            return (_context.CompletedWorkout?.Any(e => e.CompletedWorkoutID == id)).GetValueOrDefault();
        }
    }
}
