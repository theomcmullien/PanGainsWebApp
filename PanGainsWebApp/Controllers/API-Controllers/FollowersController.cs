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
    public class FollowersController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public FollowersController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Followers/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetFollower(int accountID)
        {
            IEnumerable<Social> socialsList = await _context.Social.ToListAsync();
            IEnumerable<Account> accountsList = await _context.Account.ToListAsync();

            int[] followerIDs = socialsList.Where(s => s.FollowingID == accountID).Select(s => s.AccountID).ToArray();
            Account[] accounts = accountsList.Where(a => followerIDs.Contains(a.AccountID)).ToArray();

            if (accounts == null) return NotFound();

            return accounts;
        }

    }
}
