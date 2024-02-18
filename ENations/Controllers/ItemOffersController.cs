using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class ItemOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemOffers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemOffers.Include(i => i.Country).Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItemOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ItemOffers == null)
            {
                return NotFound();
            }

            var itemOffers = await _context.ItemOffers
                .Include(i => i.Country)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.ItemOffersId == id);
            if (itemOffers == null)
            {
                return NotFound();
            }

            return View(itemOffers);
        }

        // GET: ItemOffers/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: ItemOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemOffersId,Item,Price,Quantity,Quality,CountryId,UserId")] string Item, decimal Price, int Quality, int Quantity, int CountryId, int UserId)
        {
            if (ModelState.IsValid)
            {
                var itemOffers = new ItemOffers();
                itemOffers.CountryId = CountryId;
                itemOffers.UserId = UserId;
                itemOffers.Quantity = Quantity;
                itemOffers.Item = Item;
                itemOffers.Price = Price;
                itemOffers.Quality = Quality;
                _context.Add(itemOffers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", itemOffers.CountryId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", itemOffers.UserId);
            return View();
        }

        // GET: ItemOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemOffers == null)
            {
                return NotFound();
            }

            var itemOffers = await _context.ItemOffers.FindAsync(id);
            if (itemOffers == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Name", itemOffers.CountryId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", itemOffers.UserId);
            return View(itemOffers);
        }

        // POST: ItemOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemOffersId,Item,Price,Quantity,Quality,CountryId,UserId")] string Item, decimal Price, int Quality, int Quantity, int CountryId, int UserId)
        {

            if (ModelState.IsValid)
            {
                var itemOffers = await _context.ItemOffers.FirstOrDefaultAsync(m => m.ItemOffersId == id);
                itemOffers.CountryId = CountryId;
                itemOffers.UserId = UserId;
                itemOffers.Quantity = Quantity;
                itemOffers.Item = Item;
                itemOffers.Price = Price;
                itemOffers.Quality = Quality;
                _context.Update(itemOffers);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", itemOffers.CountryId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", itemOffers.UserId);
            return View();
        }

        // GET: ItemOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ItemOffers == null)
            {
                return NotFound();
            }

            var itemOffers = await _context.ItemOffers
                .Include(i => i.Country)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.ItemOffersId == id);
            if (itemOffers == null)
            {
                return NotFound();
            }

            return View(itemOffers);
        }

        // POST: ItemOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ItemOffers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ItemOffers'  is null.");
            }
            var itemOffers = await _context.ItemOffers.FindAsync(id);
            if (itemOffers != null)
            {
                _context.ItemOffers.Remove(itemOffers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemOffersExists(int id)
        {
            return (_context.ItemOffers?.Any(e => e.ItemOffersId == id)).GetValueOrDefault();
        }
    }
}
