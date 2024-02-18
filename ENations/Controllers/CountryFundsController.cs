using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class CountryFundsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryFundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CountryFunds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CountryFunds.Include(c => c.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CountryFunds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CountryFunds == null)
            {
                return NotFound();
            }

            var countryFunds = await _context.CountryFunds
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (countryFunds == null)
            {
                return NotFound();
            }

            return View(countryFunds);
        }

        // GET: CountryFunds/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name");
            return View();
        }

        // POST: CountryFunds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,Currency,Gold")] int CountryId, string Currency, decimal Gold)
        {
            if (ModelState.IsValid)
            {
                var countryFunds = new CountryFunds();
                countryFunds.CountryId = CountryId;
                countryFunds.Currency = Currency;
                countryFunds.Gold = Gold;
                _context.Add(countryFunds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", countryFunds.CountryId);
            return View();
        }

        // GET: CountryFunds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CountryFunds == null)
            {
                return NotFound();
            }

            var countryFunds = await _context.CountryFunds.FindAsync(id);
            if (countryFunds == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name", countryFunds.CountryId);
            return View(countryFunds);
        }

        // POST: CountryFunds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,Currency,Gold")] int CountryId, string Currency, decimal Gold)
        {


            if (ModelState.IsValid)
            {
                var countryFunds = await _context.CountryFunds.FirstOrDefaultAsync(m => m.CountryId == id);
                countryFunds.CountryId = CountryId;
                countryFunds.Currency = Currency;
                countryFunds.Gold = Gold;
                _context.Update(countryFunds);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", countryFunds.CountryId);
            return View();
        }

        // GET: CountryFunds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CountryFunds == null)
            {
                return NotFound();
            }

            var countryFunds = await _context.CountryFunds
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (countryFunds == null)
            {
                return NotFound();
            }

            return View(countryFunds);
        }

        // POST: CountryFunds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CountryFunds == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CountryFunds'  is null.");
            }
            var countryFunds = await _context.CountryFunds.FindAsync(id);
            if (countryFunds != null)
            {
                _context.CountryFunds.Remove(countryFunds);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryFundsExists(int id)
        {
            return (_context.CountryFunds?.Any(e => e.CountryId == id)).GetValueOrDefault();
        }
    }
}
