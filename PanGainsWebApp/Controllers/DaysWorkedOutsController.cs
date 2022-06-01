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
    public class DaysWorkedOutsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public DaysWorkedOutsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: DaysWorkedOuts
        public async Task<IActionResult> Index()
        {
            return _context.DaysWorkedOut != null ?
                        View(await _context.DaysWorkedOut.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.DaysWorkedOut'  is null.");
        }

        // GET: DaysWorkedOuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DaysWorkedOut == null)
            {
                return NotFound();
            }

            var daysWorkedOut = await _context.DaysWorkedOut
                .FirstOrDefaultAsync(m => m.DaysWorkedOutID == id);
            if (daysWorkedOut == null)
            {
                return NotFound();
            }

            return View(daysWorkedOut);
        }

        // GET: DaysWorkedOuts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DaysWorkedOuts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DaysWorkedOutID,AccountID,Day,Hours")] DaysWorkedOut daysWorkedOut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daysWorkedOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(daysWorkedOut);
        }

        // GET: DaysWorkedOuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DaysWorkedOut == null)
            {
                return NotFound();
            }

            var daysWorkedOut = await _context.DaysWorkedOut.FindAsync(id);
            if (daysWorkedOut == null)
            {
                return NotFound();
            }
            return View(daysWorkedOut);
        }

        // POST: DaysWorkedOuts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DaysWorkedOutID,AccountID,Day,Hours")] DaysWorkedOut daysWorkedOut)
        {
            if (id != daysWorkedOut.DaysWorkedOutID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(daysWorkedOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaysWorkedOutExists(daysWorkedOut.DaysWorkedOutID))
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
            return View(daysWorkedOut);
        }

        // GET: DaysWorkedOuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DaysWorkedOut == null)
            {
                return NotFound();
            }

            var daysWorkedOut = await _context.DaysWorkedOut
                .FirstOrDefaultAsync(m => m.DaysWorkedOutID == id);
            if (daysWorkedOut == null)
            {
                return NotFound();
            }

            return View(daysWorkedOut);
        }

        // POST: DaysWorkedOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DaysWorkedOut == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.DaysWorkedOut'  is null.");
            }
            var daysWorkedOut = await _context.DaysWorkedOut.FindAsync(id);
            if (daysWorkedOut != null)
            {
                _context.DaysWorkedOut.Remove(daysWorkedOut);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DaysWorkedOutExists(int id)
        {
            return (_context.DaysWorkedOut?.Any(e => e.DaysWorkedOutID == id)).GetValueOrDefault();
        }
    }
}
