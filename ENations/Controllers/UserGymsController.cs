using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ENations.Models;

namespace ENations.Controllers
{
    public class UserGymsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserGymsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserGyms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserGyms.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserGyms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserGyms == null)
            {
                return NotFound();
            }

            var userGyms = await _context.UserGyms
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userGyms == null)
            {
                return NotFound();
            }

            return View(userGyms);
        }

        // GET: UserGyms/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserGyms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId")] UserGyms userGyms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userGyms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userGyms.UserId);
            return View(userGyms);
        }

        // GET: UserGyms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserGyms == null)
            {
                return NotFound();
            }

            var userGyms = await _context.UserGyms.FindAsync(id);
            if (userGyms == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userGyms.UserId);
            return View(userGyms);
        }

        // POST: UserGyms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId")] UserGyms userGyms)
        {
            if (id != userGyms.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGyms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGymsExists(userGyms.UserId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userGyms.UserId);
            return View(userGyms);
        }

        // GET: UserGyms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserGyms == null)
            {
                return NotFound();
            }

            var userGyms = await _context.UserGyms
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userGyms == null)
            {
                return NotFound();
            }

            return View(userGyms);
        }

        // POST: UserGyms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserGyms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserGyms'  is null.");
            }
            var userGyms = await _context.UserGyms.FindAsync(id);
            if (userGyms != null)
            {
                _context.UserGyms.Remove(userGyms);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGymsExists(int id)
        {
          return (_context.UserGyms?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
