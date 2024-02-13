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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Users.Include(u => u.Region);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _context.Users
                .Include(u => u.Region)
                .FirstOrDefaultAsync(m => m.UserId == id);

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Name");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Strength,Username,Email,Password,Level,Xp,RegionId")] int Strength, string Username, string Email, string Password, int Level, int Xp, int RegionId)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.Strength = Strength;
                user.Username = Username;
                user.Email = Email;
                user.Password = Password;
                user.Level = Level;
                user.Xp = Xp;
                user.RegionId = RegionId;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var user = await _context.Users.FindAsync(id);

            ViewData["RegionId"] = new SelectList(_context.Regions, "RegionId", "Name", user.RegionId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Strength,Username,Email,Password,Level,Xp,RegionId")] int Strength, string Username, string Email, string Password, int Level, int Xp, int RegionId)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(m=>m.UserId.Equals(id));
                user.Strength = Strength;
                user.Username = Username;
                user.Email = Email;
                user.Password = Password;
                user.Level = Level;
                user.Xp = Xp;
                user.RegionId = RegionId; _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _context.Users
                .Include(u => u.Region)
                .FirstOrDefaultAsync(m => m.UserId == id);

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
