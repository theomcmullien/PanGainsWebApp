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
    public class UserAuthsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public UserAuthsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/UserAuths
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAuth>>> GetUserAuth()
        {
            return await _context.UserAuth.ToListAsync();
        }

        // GET: api/UserAuths/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAuth>> GetUserAuth(int id)
        {
            var userAuth = await _context.UserAuth.FindAsync(id);

            if (userAuth == null) return NotFound();

            return userAuth;
        }

        // PUT: api/UserAuths/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAuth(int id, UserAuth userAuth)
        {
            if (id != userAuth.UserAuthID) return BadRequest();

            _context.Entry(userAuth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAuthExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/UserAuths
        [HttpPost]
        public async Task<ActionResult<UserAuth>> PostUserAuth(UserAuth userAuth)
        {
            _context.UserAuth.Add(userAuth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAuth", new { id = userAuth.UserAuthID }, userAuth);
        }

        // DELETE: api/UserAuths/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAuth(int id)
        {
            var userAuth = await _context.UserAuth.FindAsync(id);

            if (userAuth == null) return NotFound();

            _context.UserAuth.Remove(userAuth);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAuthExists(int id)
        {
            return _context.UserAuth.Any(e => e.UserAuthID == id);
        }
    }
}
