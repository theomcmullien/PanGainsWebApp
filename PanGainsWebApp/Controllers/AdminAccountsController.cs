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
    public class AdminAccountsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public AdminAccountsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: AdminAccounts
        public async Task<IActionResult> Index()
        {
              return _context.AdminAccount != null ? 
                          View(await _context.AdminAccount.ToListAsync()) :
                          Problem("Entity set 'PanGainsWebAppContext.AdminAccount'  is null.");
        }

        // GET: AdminAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdminAccount == null)
            {
                return NotFound();
            }

            var adminAccount = await _context.AdminAccount
                .FirstOrDefaultAsync(m => m.AdminAccountID == id);
            if (adminAccount == null)
            {
                return NotFound();
            }

            return View(adminAccount);
        }

        // GET: AdminAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminAccountID,Username,Password")] AdminAccount adminAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminAccount);
        }

        // GET: AdminAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdminAccount == null)
            {
                return NotFound();
            }

            var adminAccount = await _context.AdminAccount.FindAsync(id);
            if (adminAccount == null)
            {
                return NotFound();
            }
            return View(adminAccount);
        }

        // POST: AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminAccountID,Username,Password")] AdminAccount adminAccount)
        {
            if (id != adminAccount.AdminAccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminAccountExists(adminAccount.AdminAccountID))
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
            return View(adminAccount);
        }

        // GET: AdminAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdminAccount == null)
            {
                return NotFound();
            }

            var adminAccount = await _context.AdminAccount
                .FirstOrDefaultAsync(m => m.AdminAccountID == id);
            if (adminAccount == null)
            {
                return NotFound();
            }

            return View(adminAccount);
        }

        // POST: AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdminAccount == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.AdminAccount'  is null.");
            }
            var adminAccount = await _context.AdminAccount.FindAsync(id);
            if (adminAccount != null)
            {
                _context.AdminAccount.Remove(adminAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminAccountExists(int id)
        {
          return (_context.AdminAccount?.Any(e => e.AdminAccountID == id)).GetValueOrDefault();
        }
    }
}
