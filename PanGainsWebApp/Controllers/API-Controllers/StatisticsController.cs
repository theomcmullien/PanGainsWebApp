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
    public class StatisticsController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public StatisticsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Statistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Statistics>>> GetStatistics()
        {
            return await _context.Statistics.ToListAsync();
        }

        // GET: api/Statistics/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<Statistics>> GetStatistics(int accountID)
        {
            IEnumerable<Statistics> statisticsList = await _context.Statistics.ToListAsync();
            Statistics statistics = statisticsList.Where(s => s.AccountID == accountID).First();

            if (statistics == null) return NotFound();

            return statistics;
        }

        // PUT: api/Statistics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatistics(int id, Statistics statistics)
        {
            if (id != statistics.StatisticsID) return BadRequest();

            _context.Entry(statistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatisticsExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Statistics
        [HttpPost]
        public async Task<ActionResult<Statistics>> PostStatistics(Statistics statistics)
        {
            _context.Statistics.Add(statistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatistics", new { id = statistics.StatisticsID }, statistics);
        }

        // DELETE: api/Statistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatistics(int id)
        {
            var statistics = await _context.Statistics.FindAsync(id);

            if (statistics == null) return NotFound();

            _context.Statistics.Remove(statistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatisticsExists(int id)
        {
            return _context.Statistics.Any(e => e.StatisticsID == id);
        }
    }
}
