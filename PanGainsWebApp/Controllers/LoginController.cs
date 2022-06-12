using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;
using System.Diagnostics;

namespace PanGainsWebApp.Controllers
{
    public class LoginController : Controller
    {
        const int DASHBOARD_ENTRY_COUNT = 6;

        private readonly PanGainsWebAppContext _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(PanGainsWebAppContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string username, string password)
        {
            bool isAdmin = false;
            //string displayUsername = "";
            //string displayPassword = "";

            if (username != null)
            {
                foreach (AdminAccount a in await _context.AdminAccount.ToListAsync())
                {
                    if (a.Username.ToLower() == username.ToLower() && a.Password.ToLower() == password.ToLower())
                    {
                        isAdmin = true;
                        //displayUsername = a.Username;
                        //displayPassword = a.Password;
                    }
                }
            }
            
            if (isAdmin)
            {
                //Change HomeController too

                var accountsList = await _context.Account.ToListAsync();
                var exercisesList = await _context.Exercise.ToListAsync();
                var challengesList = await _context.Challenges.ToListAsync();
                var leaderboardsList = await _context.Leaderboard.ToListAsync();
                var challengeStatsList = await _context.ChallengeStats.ToListAsync();

                DashboardDetails model = new DashboardDetails();

                model.AccountsCount = accountsList.Count();
                model.PremiumAccountsCount = accountsList.Where(a => a.Type == "Premium").ToList().Count();
                model.ExercisesCount = exercisesList.Count();
                model.ChallengesCount = challengesList.Count();

                var recentAccounts = new List<Account>();
                accountsList.Reverse();
                for (int i = 0; i < DASHBOARD_ENTRY_COUNT; i++)
                {
                    if (accountsList[i] != null)
                    {
                        recentAccounts.Add(accountsList[i]);
                    }
                }
                model.RecentAccounts = recentAccounts;


                List<LeaderboardPosition> leaderboardPositions = new List<LeaderboardPosition>();

                int leaderboardID = leaderboardsList.Where(l => l.LeaderboardDate.Month == DateTime.Now.Month && l.LeaderboardDate.Year == DateTime.Now.Year).Select(l => l.LeaderboardID).First();

                foreach (ChallengeStats c in challengeStatsList)
                {
                    if (c.LeaderboardID == leaderboardID)
                    {
                        Account a = accountsList.Where(a => a.AccountID == c.AccountID).First();
                        leaderboardPositions.Add(new LeaderboardPosition(a.Firstname, a.Lastname, c.ChallengeTotalReps));
                    }
                }

                leaderboardPositions.Sort();
                leaderboardPositions.Reverse();

                List<LeaderboardPosition> topSix = new List<LeaderboardPosition>();
                for (int i = 0; i < DASHBOARD_ENTRY_COUNT; i++)
                {
                    if (leaderboardPositions[i] != null)
                    {
                        topSix.Add(leaderboardPositions[i]);
                    }

                }
                model.LeaderboardPositions = topSix;

                //Change HomeController too

                return View("/Views/Home/Index.cshtml", model);
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}