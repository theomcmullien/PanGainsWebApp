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
    public class AccountsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public AccountsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var model = new ListModel();
            model.AccountModel = await _context.Account.ToListAsync();
            model.ChallengeStatsModel = await _context.ChallengeStats.ToListAsync();
            model.CompletedWorkoutModel = await _context.CompletedWorkout.ToListAsync();
            model.DaysWorkedOutModel = await _context.DaysWorkedOut.ToListAsync();
            model.ExerciseModel = await _context.Exercise.ToListAsync();
            model.FolderModel = await _context.Folder.ToListAsync();
            model.LeaderboardModel = await _context.Leaderboard.ToListAsync();
            model.RoutineModel = await _context.Routine.ToListAsync();
            model.SetModel = await _context.Set.ToListAsync();
            model.SocialModel = await _context.Social.ToListAsync();
            model.StatisticsModel = await _context.Statistics.ToListAsync();
            model.YourExerciseModel = await _context.YourExercise.ToListAsync();
            //model.Username = "Admin";
            //model.Password = "admin";
            return View(model);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountID,Firstname,Lastname,Email,PasswordHash,PasswordSalt,Title,ProfilePicture,Description,Private,Notifications,AverageChallengePos,Type,Role")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountID,Firstname,Lastname,Email,PasswordHash,PasswordSalt,Title,ProfilePicture,Description,Private,Notifications,AverageChallengePos,Type,Role")] Account account)
        {
            if (id != account.AccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountID))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.AccountID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Account == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.Account'  is null.");
            }
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                _context.Account.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return (_context.Account?.Any(e => e.AccountID == id)).GetValueOrDefault();
        }
    }
}
