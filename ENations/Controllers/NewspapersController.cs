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
    public class NewspapersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewspapersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Newspapers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Newspapers.Include(n => n.Founder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Newspapers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var newspaper = await _context.Newspapers
                .Include(n => n.Founder)
                .FirstOrDefaultAsync(m => m.NewspaperId == id);

            return View(newspaper);
        }

        // GET: Newspapers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: Newspapers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewspaperId,Description,Name,UserId")] string Description, string Name, int UserId)
        {
            if (ModelState.IsValid)
            {
                var newspaper = new Newspaper();
                newspaper.Description = Description;
                newspaper.Name = Name;
                newspaper.UserId = UserId;
                _context.Add(newspaper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Newspapers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var newspaper = await _context.Newspapers.FindAsync(id);

            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", newspaper.UserId);
            return View(newspaper);
        }

        // POST: Newspapers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewspaperId,Description,Name,UserId")] string Description, string Name, int UserId)
        {

            if (ModelState.IsValid)
            {
                var newspaper = await _context.Newspapers.FirstOrDefaultAsync(x => x.NewspaperId.Equals(id));
                newspaper.Description = Description;
                newspaper.Name = Name;
                newspaper.UserId = UserId;
                _context.Update(newspaper);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        // GET: Newspapers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var newspaper = await _context.Newspapers
                .Include(n => n.Founder)
                .FirstOrDefaultAsync(m => m.NewspaperId == id);

            return View(newspaper);
        }

        // POST: Newspapers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newspaper = await _context.Newspapers.FindAsync(id);
            if (newspaper != null)
            {
                _context.Newspapers.Remove(newspaper);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewspaperExists(int id)
        {
            return (_context.Newspapers?.Any(e => e.NewspaperId == id)).GetValueOrDefault();
        }
    }
}
