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
    public class FoldersController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public FoldersController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Folders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolder()
        {
            return await _context.Folder.ToListAsync();
        }

        // GET: api/Folders/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetFolder(int accountID)
        {
            IEnumerable<Folder> foldersList = await _context.Folder.ToListAsync();
            List<Folder> folders = foldersList.Where(f => accountID == f.AccountID).ToList();

            if (folders == null) return NotFound();

            return folders;
        }

        // PUT: api/Folders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolder(int id, Folder folder)
        {
            if (id != folder.FolderID) return BadRequest();

            _context.Entry(folder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FolderExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Folders
        [HttpPost]
        public async Task<ActionResult<Folder>> PostFolder(Folder folder)
        {
            _context.Folder.Add(folder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolder", new { id = folder.FolderID }, folder);
        }

        // DELETE: api/Folders/5
        [HttpDelete("{folderID}")]
        public async Task<IActionResult> DeleteFolder(int folderID)
        {
            //remove Routine
            IEnumerable<Routine> routinesList = await _context.Routine.ToListAsync();
            List<Routine> routines = routinesList.Where(r => r.FolderID == folderID).ToList();
            if (routines.Any()) foreach (Routine r in routines) _context.Routine.Remove(r);

            //remove Folder
            Folder folder = await _context.Folder.FindAsync(folderID);
            if (folder == null) return NotFound();
            _context.Folder.Remove(folder);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FolderExists(int id)
        {
            return _context.Folder.Any(e => e.FolderID == id);
        }
    }
}
