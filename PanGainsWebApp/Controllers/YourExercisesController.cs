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
    public class YourExercisesController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public YourExercisesController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: YourExercises
        public async Task<IActionResult> Index()
        {
            return _context.YourExercise != null ?
                        View(await _context.YourExercise.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.YourExercise'  is null.");
        }

        // GET: YourExercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.YourExercise == null)
            {
                return NotFound();
            }

            var yourExercise = await _context.YourExercise
                .FirstOrDefaultAsync(m => m.YourExerciseID == id);
            if (yourExercise == null)
            {
                return NotFound();
            }

            return View(yourExercise);
        }

        // GET: YourExercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YourExercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YourExerciseID,RoutineID,ExerciseID")] YourExercise yourExercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yourExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yourExercise);
        }

        // GET: YourExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.YourExercise == null)
            {
                return NotFound();
            }

            var yourExercise = await _context.YourExercise.FindAsync(id);
            if (yourExercise == null)
            {
                return NotFound();
            }
            return View(yourExercise);
        }

        // POST: YourExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YourExerciseID,RoutineID,ExerciseID")] YourExercise yourExercise)
        {
            if (id != yourExercise.YourExerciseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yourExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YourExerciseExists(yourExercise.YourExerciseID))
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
            return View(yourExercise);
        }

        // GET: YourExercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.YourExercise == null)
            {
                return NotFound();
            }

            var yourExercise = await _context.YourExercise
                .FirstOrDefaultAsync(m => m.YourExerciseID == id);
            if (yourExercise == null)
            {
                return NotFound();
            }

            return View(yourExercise);
        }

        // POST: YourExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.YourExercise == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.YourExercise'  is null.");
            }
            var yourExercise = await _context.YourExercise.FindAsync(id);
            if (yourExercise != null)
            {
                _context.YourExercise.Remove(yourExercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YourExerciseExists(int id)
        {
            return (_context.YourExercise?.Any(e => e.YourExerciseID == id)).GetValueOrDefault();
        }
    }
}
