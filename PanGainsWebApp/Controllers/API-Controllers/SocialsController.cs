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
    public class SocialsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public SocialsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Socials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Social>>> GetSocial()
        {
            return await _context.Social.ToListAsync();
        }

        // GET: api/Socials/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetFollowing(int accountID)
        {
            IEnumerable<Social> socialsList = await _context.Social.ToListAsync();
            IEnumerable<Account> accountsList = await _context.Account.ToListAsync();

            int[] followingIDs = socialsList.Where(s => s.AccountID == accountID).Select(s => s.FollowingID).ToArray();
            Account[] accounts = accountsList.Where(a => followingIDs.Contains(a.AccountID)).ToArray();

            if (accounts == null) return NotFound();

            return accounts;
        }

        // PUT: api/Socials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocial(int id, Social social)
        {
            if (id != social.SocialID) return BadRequest();

            _context.Entry(social).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Socials
        [HttpPost]
        public async Task<ActionResult<Social>> PostSocial(Social social)
        {
            _context.Social.Add(social);
            await _context.SaveChangesAsync();

            return Ok(social);
        }

        // DELETE: api/Socials/5
        [HttpDelete("{accountID}")]
        public async Task<IActionResult> DeleteSocial(int accountID, int followingID)
        {
            Social social = _context.Social.Where(s => s.AccountID == accountID && s.FollowingID == followingID).First();

            if (social == null) return NotFound();

            _context.Social.Remove(social);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SocialExists(int id)
        {
            return _context.Social.Any(e => e.SocialID == id);
        }
    }
}
