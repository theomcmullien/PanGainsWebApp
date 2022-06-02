using Microsoft.AspNetCore.Mvc;
using PanGainsWebApp.Models;
using System.Diagnostics;

namespace PanGainsWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return View("/Views/Home/Index.cshtml");
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