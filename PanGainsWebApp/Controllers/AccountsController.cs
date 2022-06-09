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
        public async Task<IActionResult> Index(string? searchAccount)
        {
            var accountsList = await _context.Account.ToListAsync();
            if (searchAccount != null)
            {
                if (int.TryParse(searchAccount, out int s))
                {
                    accountsList = accountsList.Where(a => a.AccountID == s).ToList();
                }
                else
                {
                    accountsList = accountsList.Where(a => string.Format("{0} {1}", a.Firstname, a.Lastname) == searchAccount || a.Firstname == searchAccount || a.Lastname == searchAccount || a.Email == searchAccount || a.Title == searchAccount || a.Type == searchAccount).ToList();
                }
                
            }

            var model = new ListModel();
            model.AccountModel = accountsList;
            
            return View(model);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? accountID)
        {
            if (accountID == null) return NotFound(); //error checking

            AccountDetails accountDetails = new AccountDetails();

            List<Account> accountsList = await _context.Account.ToListAsync();
            List<Statistics> statisticsList = await _context.Statistics.ToListAsync();
            List<DaysWorkedOut> daysWorkedOutList = await _context.DaysWorkedOut.ToListAsync();
            List<Social> socialList = await _context.Social.ToListAsync();

            accountDetails.Account = accountsList.Where(a => a.AccountID == accountID).First();
            accountDetails.AccountID = accountDetails.Account.AccountID;
            accountDetails.Statistics = statisticsList.Where(s => s.AccountID == accountID).First();
            accountDetails.DaysWorkedOutList = daysWorkedOutList.Where(d => d.AccountID == accountID).ToList();
            accountDetails.Followers = socialList.Where(s => s.FollowingID == accountID).ToList().Count();
            accountDetails.Following = socialList.Where(s => s.AccountID == accountID).ToList().Count();

            if (accountDetails == null) return NotFound(); //error checking

            return View(accountDetails);
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
        public async Task<IActionResult> Edit(int? accountID)
        {
            if (accountID == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(accountID);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int accountID, [Bind("AccountID,Firstname,Lastname,Email,Password,Title,ProfilePicture,Description,Private,Notifications,AverageChallengePos,Type,Role")] Account account)
        {
            if (accountID != account.AccountID)
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
        public async Task<IActionResult> Delete(int? accountID)
        {
            if (accountID == null) return NotFound(); //error checking

            AccountDetails accountDetails = new AccountDetails();

            List<DaysWorkedOut> daysWorkedOutList = await _context.DaysWorkedOut.ToListAsync();
            List<Social> socialList = await _context.Social.ToListAsync();

            accountDetails.Account = await _context.Account.FirstOrDefaultAsync(a => a.AccountID == accountID);
            accountDetails.AccountID = accountDetails.Account.AccountID;
            accountDetails.Statistics = await _context.Statistics.FirstOrDefaultAsync(s => s.AccountID == accountID);

            accountDetails.DaysWorkedOutList = daysWorkedOutList.Where(d => d.AccountID == accountID).ToList();
            accountDetails.Followers = socialList.Where(s => s.FollowingID == accountID).ToList().Count();
            accountDetails.Following = socialList.Where(s => s.AccountID == accountID).ToList().Count();

            if (accountDetails == null) return NotFound(); //error checking

            return View(accountDetails);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int accountID)
        {
            var account = await _context.Account.FirstOrDefaultAsync(a => a.AccountID == accountID);
            _context.Account.Remove(account);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return (_context.Account?.Any(e => e.AccountID == id)).GetValueOrDefault();
        }
    }
}
