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
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return _context.Countries != null ?
                        View(await _context.Countries.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Countries'  is null.");
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,Capital,Currency,Name")] string Capital, string Currency, string Name)
        {
            if (ModelState.IsValid)
            {
                var country = new Country();
                country.Name = Name;
                country.Capital = Capital;
                country.Currency = Currency;
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var country = await _context.Countries.FindAsync(id);
            return View(country);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,Capital,Currency,Name")] string Capital, string Currency, string Name)
        {
            if (ModelState.IsValid)
            {
                var country = await _context.Countries
                                    .FirstOrDefaultAsync(m => m.CountryId == id);
                country.Capital = Capital;
                country.Name = Name;
                country.Currency = Currency;
                _context.Update(country);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.CountryId == id)).GetValueOrDefault();
        }
    }
}
