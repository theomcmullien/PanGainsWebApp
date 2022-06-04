using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;
using System.Diagnostics;

namespace PanGainsWebApp.Controllers
{
    public class LoginController : Controller
    {
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
                //model.Username = displayUsername;
                //model.Password = displayPassword;
                
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