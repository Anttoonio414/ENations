using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class RegionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Regions.Include(r => r.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Regions == null)
            {
                return NotFound();
            }

            var region = await _context.Regions
                .Include(r => r.Country)
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name");
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionId,Name,CountryId")] string Name, int CountryId)
        {
            if (ModelState.IsValid)
            {
                var region = new Region();
                region.Name = Name;
                region.CountryId = CountryId;
                _context.Add(region);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", region.CountryId);
            return View();
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var region = await _context.Regions.FindAsync(id);

            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name", region.CountryId);
            return View(region);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,Name,CountryId")] string Name, int CountryId)
        {
            if (ModelState.IsValid)
            {
                var region = await _context.Regions
                    .FirstOrDefaultAsync(m => m.RegionId == id);
                region.CountryId = CountryId;
                region.Name = Name;
                _context.Update(region);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", region.CountryId);
            return View();
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Regions == null)
            {
                return NotFound();
            }

            var region = await _context.Regions
                .Include(r => r.Country)
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region != null)
            {
                _context.Regions.Remove(region);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionExists(int id)
        {
            return (_context.Regions?.Any(e => e.RegionId == id)).GetValueOrDefault();
        }
    }
}
