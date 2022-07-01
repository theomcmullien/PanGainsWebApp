using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;
using StreamChat.Clients;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PanGainsWebApp.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly PanGainsWebAppContext context;

        public AuthController(IConfiguration configuration, PanGainsWebAppContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> Register(AccountAuth request)
        {
            var accountsList = await context.Account.ToListAsync();
            foreach (var a in accountsList) if (a.Email == request.Email) return BadRequest("Email already exists");

            int maxAccountID = 0;
            foreach (Account a in await context.Account.ToListAsync()) if (a.AccountID > maxAccountID) maxAccountID = a.AccountID;

            maxAccountID++;
            
            Account account = new Models.Account();
            account.AccountID = maxAccountID;
            account.Firstname = request.Firstname;
            account.Lastname = request.Lastname;
            account.Email = request.Email;
            account.Title = "";
            account.ProfilePicture = "";
            account.Description = "";
            account.Private = false;
            account.Notifications = false;
            account.AverageChallengePos = 0;
            account.Type = "";
            account.Role = "Account";
            account.Password = request.Password;
            account.MessageToken = getToken(account);

            context.Account.Add(account);

            int maxStatisticsID = 0;
            foreach (Statistics s in await context.Statistics.ToListAsync()) if (s.StatisticsID > maxStatisticsID) maxStatisticsID = s.StatisticsID;
            maxStatisticsID++;

            Statistics statistics = new Statistics();
            statistics.StatisticsID = maxStatisticsID;
            statistics.AccountID = maxAccountID;
            statistics.TotalLifted = 0;
            statistics.AvgSets = statistics.AvgReps = statistics.AvgWorkoutTime = statistics.TotalWorkouts = 0;
            context.Statistics.Add(statistics);

            string[] days = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            for (int i = 0; i < days.Length; i++)
            {
                int maxDaysWorkedOutID = 0;
                foreach (DaysWorkedOut d1 in await context.DaysWorkedOut.ToListAsync()) if (d1.DaysWorkedOutID > maxDaysWorkedOutID) maxDaysWorkedOutID = d1.DaysWorkedOutID;
                maxDaysWorkedOutID++;

                DaysWorkedOut d = new DaysWorkedOut();
                d.DaysWorkedOutID = maxDaysWorkedOutID;
                d.AccountID = maxAccountID;
                d.Day = days[i];
                d.Hours = 0;
                context.DaysWorkedOut.Add(d);

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return Ok(account);
        }

        private string? getToken(Account account)
        {
            var factory = new StreamClientFactory("u4ycx472g2x3", "dbk3mmd7hvebwzefnnwpnhe6f2wcfbhvnnvf95p4qtacx9un29bbjbyyvngd6hdc");
            var userClient = factory.GetUserClient();

            return userClient.CreateToken($"{account.Firstname}-{account.Lastname}");
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login(Login request)
        {
            //search for user
            var account = context.Account.FirstOrDefault(u => u.Email == request.Email);
            if (account == null)
            {
                return BadRequest("User not found");
            }
            if (account.Password != request.Password)
            {
                return BadRequest("Wrong Password");

            }
            string token = CreateToken(account);
            return Ok(token);
        }

        private string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, account.Firstname),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }

}

