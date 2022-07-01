using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAccountsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public AdminAccountsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/AdminAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminAccount>>> GetAdminAccount()
        {
          if (_context.AdminAccount == null)
          {
              return NotFound();
          }
            return await _context.AdminAccount.ToListAsync();
        }

        // GET: api/AdminAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminAccount>> GetAdminAccount(int id)
        {
          if (_context.AdminAccount == null)
          {
              return NotFound();
          }
            var adminAccount = await _context.AdminAccount.FindAsync(id);

            if (adminAccount == null)
            {
                return NotFound();
            }

            return adminAccount;
        }

        // PUT: api/AdminAccounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminAccount(int id, AdminAccount adminAccount)
        {
            if (id != adminAccount.AdminAccountID)
            {
                return BadRequest();
            }

            _context.Entry(adminAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AdminAccounts
        [HttpPost]
        public async Task<ActionResult<AdminAccount>> PostAdminAccount(AdminAccount adminAccount)
        {
          if (_context.AdminAccount == null)
          {
              return Problem("Entity set 'PanGainsAPIContext.AdminAccount'  is null.");
          }
            _context.AdminAccount.Add(adminAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminAccount", new { id = adminAccount.AdminAccountID }, adminAccount);
        }

        // DELETE: api/AdminAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminAccount(int id)
        {
            if (_context.AdminAccount == null)
            {
                return NotFound();
            }
            var adminAccount = await _context.AdminAccount.FindAsync(id);
            if (adminAccount == null)
            {
                return NotFound();
            }

            _context.AdminAccount.Remove(adminAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminAccountExists(int id)
        {
            return (_context.AdminAccount?.Any(e => e.AdminAccountID == id)).GetValueOrDefault();
        }
    }
}
