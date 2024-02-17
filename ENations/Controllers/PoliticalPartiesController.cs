using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class PoliticalPartiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoliticalPartiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PoliticalParties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PoliticalParties.Include(p => p.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PoliticalParties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PoliticalParties == null)
            {
                return NotFound();
            }

            var politicalParty = await _context.PoliticalParties
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.PoliticalPartyId == id);
            if (politicalParty == null)
            {
                return NotFound();
            }

            return View(politicalParty);
        }

        // GET: PoliticalParties/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name");
            return View();
        }

        // POST: PoliticalParties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoliticalPartyId,Name,Description,CountryId")] string Name, string Description, int CountryId)
        {
            if (ModelState.IsValid)
            {
                var politicalParty = new PoliticalParty();
                politicalParty.Name = Name;
                politicalParty.Description = Description;
                politicalParty.CountryId = CountryId;
                _context.Add(politicalParty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", politicalParty.CountryId);
            return View();
        }

        // GET: PoliticalParties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PoliticalParties == null)
            {
                return NotFound();
            }

            var politicalParty = await _context.PoliticalParties.FindAsync(id);
            if (politicalParty == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name", politicalParty.CountryId);
            return View(politicalParty);
        }

        // POST: PoliticalParties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoliticalPartyId,Name,Description,CountryId")] string Name, string Description, int CountryId)
        {

            if (ModelState.IsValid)
            {
                var politicalParty = await _context.PoliticalParties.FirstOrDefaultAsync(m => m.PoliticalPartyId == id);
                politicalParty.Name = Name;
                politicalParty.Description = Description;
                politicalParty.CountryId = CountryId;
                _context.Update(politicalParty);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", politicalParty.CountryId);
            return View();
        }

        // GET: PoliticalParties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PoliticalParties == null)
            {
                return NotFound();
            }

            var politicalParty = await _context.PoliticalParties
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.PoliticalPartyId == id);
            if (politicalParty == null)
            {
                return NotFound();
            }

            return View(politicalParty);
        }

        // POST: PoliticalParties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PoliticalParties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PoliticalParties'  is null.");
            }
            var politicalParty = await _context.PoliticalParties.FindAsync(id);
            if (politicalParty != null)
            {
                _context.PoliticalParties.Remove(politicalParty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliticalPartyExists(int id)
        {
            return (_context.PoliticalParties?.Any(e => e.PoliticalPartyId == id)).GetValueOrDefault();
        }
    }
}
