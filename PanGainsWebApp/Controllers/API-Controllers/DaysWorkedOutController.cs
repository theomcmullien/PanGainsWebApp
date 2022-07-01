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
    public class DaysWorkedOutController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public DaysWorkedOutController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/DaysWorkedOuts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DaysWorkedOut>>> GetDaysWorkedOut()
        {
            return await _context.DaysWorkedOut.ToListAsync();
        }

        // GET: api/DaysWorkedOuts/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<IEnumerable<DaysWorkedOut>>> GetDaysWorkedOut(int accountID)
        {
            IEnumerable<DaysWorkedOut> daysWorkedOutList = await _context.DaysWorkedOut.ToListAsync();
            DaysWorkedOut[] daysWorkedOut = daysWorkedOutList.Where(d => d.AccountID == accountID).ToArray();

            if (daysWorkedOut == null) return NotFound();

            return daysWorkedOut;
        }

        // PUT: api/DaysWorkedOuts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDaysWorkedOut(int id, DaysWorkedOut daysWorkedOut)
        {
            if (id != daysWorkedOut.DaysWorkedOutID) return BadRequest();

            _context.Entry(daysWorkedOut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DaysWorkedOutExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/DaysWorkedOuts
        [HttpPost]
        public async Task<ActionResult<DaysWorkedOut>> PostDaysWorkedOut(DaysWorkedOut daysWorkedOut)
        {
            _context.DaysWorkedOut.Add(daysWorkedOut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDaysWorkedOut", new { id = daysWorkedOut.DaysWorkedOutID }, daysWorkedOut);
        }

        // DELETE: api/DaysWorkedOuts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDaysWorkedOut(int id)
        {
            var daysWorkedOut = await _context.DaysWorkedOut.FindAsync(id);

            if (daysWorkedOut == null) return NotFound();

            _context.DaysWorkedOut.Remove(daysWorkedOut);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DaysWorkedOutExists(int id)
        {
            return _context.DaysWorkedOut.Any(e => e.DaysWorkedOutID == id);
        }
    }
}
