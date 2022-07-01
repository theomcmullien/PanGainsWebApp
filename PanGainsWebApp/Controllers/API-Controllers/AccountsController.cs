#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public AccountsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        {
            return await _context.Account.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Account>> GetAccount(string email)
        {
            var accountsList = await _context.Account.ToListAsync();
            Account account = accountsList.Where(e => e.Email == email).First();

            if (account == null) return NotFound();

            return account;
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountID) return BadRequest();

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            int maxAccountID = 0;
            foreach (Account a in await _context.Account.ToListAsync()) if (a.AccountID > maxAccountID) maxAccountID = a.AccountID;

            Statistics statistics = new Statistics();
            statistics.AccountID = maxAccountID;
            _context.Statistics.Add(statistics);

            string[] days = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            for (int i = 0; i < days.Length; i++)
            {
                DaysWorkedOut d = new DaysWorkedOut();
                d.AccountID = maxAccountID;
                d.Day = days[i];
                _context.DaysWorkedOut.Add(d);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.AccountID }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{accountID}")]
        public async Task<IActionResult> DeleteAccount(int accountID)
        {
            //remove Statistics
            IEnumerable<Statistics> statisticsList = await _context.Statistics.ToListAsync();
            List<Statistics> statistics = statisticsList.Where(s => s.AccountID == accountID).ToList();
            if (statistics.Any()) foreach (Statistics s in statistics) _context.Statistics.Remove(s);

            //remove DaysWorkedOut
            IEnumerable<DaysWorkedOut> daysWorkedOutsList = await _context.DaysWorkedOut.ToListAsync();
            List<DaysWorkedOut> daysWorkedOuts = daysWorkedOutsList.Where(d => d.AccountID == accountID).ToList();
            if (daysWorkedOuts.Any()) foreach (DaysWorkedOut d in daysWorkedOuts) _context.DaysWorkedOut.Remove(d);

            //remove ChallengeStats
            IEnumerable<ChallengeStats> challengeStatsList = await _context.ChallengeStats.ToListAsync();
            List<ChallengeStats> challengeStats = challengeStatsList.Where(c => c.AccountID == accountID).ToList();
            if (challengeStats.Any()) foreach (ChallengeStats c in challengeStats) _context.ChallengeStats.Remove(c);

            //remove Social
            IEnumerable<Social> socialsList = await _context.Social.ToListAsync();
            List<Social> socials = socialsList.Where(s => s.AccountID == accountID).ToList();
            if (socials.Any()) foreach (Social s in socials) _context.Social.Remove(s);

            //remove CompletedWorkout
            IEnumerable<CompletedWorkout> completedWorkoutsList = await _context.CompletedWorkout.ToListAsync();
            List<CompletedWorkout> completedWorkouts = completedWorkoutsList.Where(c => c.AccountID == accountID).ToList();
            if (completedWorkouts.Any()) foreach (CompletedWorkout c in completedWorkouts) _context.CompletedWorkout.Remove(c);

            //remove Folder
            IEnumerable<Folder> foldersList = await _context.Folder.ToListAsync();
            List<Folder> folders = foldersList.Where(f => f.AccountID == accountID).ToList();
            if (folders.Any()) foreach (Folder f in folders) _context.Folder.Remove(f);

            //remove Account
            Account account = await _context.Account.FindAsync(accountID);
            if (account == null) return NotFound();
            _context.Account.Remove(account);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.AccountID == id);
        }
    }
}
