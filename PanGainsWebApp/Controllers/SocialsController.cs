using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Controllers
{
    public class SocialsController : Controller
    {
        private readonly PanGainsWebAppContext _context;

        public SocialsController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: Socials
        public async Task<IActionResult> Index()
        {
            return _context.Social != null ?
                        View(await _context.Social.ToListAsync()) :
                        Problem("Entity set 'PanGainsWebAppContext.Social'  is null.");
        }

        // GET: Socials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Social == null)
            {
                return NotFound();
            }

            var social = await _context.Social
                .FirstOrDefaultAsync(m => m.SocialID == id);
            if (social == null)
            {
                return NotFound();
            }

            return View(social);
        }

        // GET: Socials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Socials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SocialID,AccountID,FollowingID")] Social social)
        {
            if (ModelState.IsValid)
            {
                _context.Add(social);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(social);
        }

        // GET: Socials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Social == null)
            {
                return NotFound();
            }

            var social = await _context.Social.FindAsync(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        // POST: Socials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SocialID,AccountID,FollowingID")] Social social)
        {
            if (id != social.SocialID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(social);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialExists(social.SocialID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(social);
        }

        // GET: Socials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Social == null)
            {
                return NotFound();
            }

            var social = await _context.Social
                .FirstOrDefaultAsync(m => m.SocialID == id);
            if (social == null)
            {
                return NotFound();
            }

            return View(social);
        }

        // POST: Socials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Social == null)
            {
                return Problem("Entity set 'PanGainsWebAppContext.Social'  is null.");
            }
            var social = await _context.Social.FindAsync(id);
            if (social != null)
            {
                _context.Social.Remove(social);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialExists(int id)
        {
            return (_context.Social?.Any(e => e.SocialID == id)).GetValueOrDefault();
        }
    }
}
