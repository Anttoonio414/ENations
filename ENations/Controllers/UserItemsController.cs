using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class UserItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserItems.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserItems == null)
            {
                return NotFound();
            }

            var userItems = await _context.UserItems
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserItemsId == id);
            if (userItems == null)
            {
                return NotFound();
            }

            return View(userItems);
        }

        // GET: UserItems/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: UserItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserItemsId,Item,Quality,Quantity,UserId")] string Item, int Quantity, int Quality, int UserId)
        {
            if (ModelState.IsValid)
            {
                var userItems = new UserItems();
                userItems.UserId = UserId;
                userItems.Quantity = Quantity;
                userItems.Quality = Quality;
                userItems.Item = Item;
                _context.Add(userItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userItems.UserId);
            return View();
        }

        // GET: UserItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserItems == null)
            {
                return NotFound();
            }

            var userItems = await _context.UserItems.FindAsync(id);
            if (userItems == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", userItems.UserId);
            return View(userItems);
        }

        // POST: UserItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserItemsId,Item,Quality,Quantity,UserId")] string Item, int Quantity, int Quality, int UserId)
        {


            if (ModelState.IsValid)
            {
                var userItems = await _context.UserItems.FirstOrDefaultAsync(m => m.UserItemsId == id);
                userItems.Item = Item;
                userItems.Quantity = Quantity;
                userItems.Quality = Quality;
                userItems.UserId = UserId;
                _context.Update(userItems);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userItems.UserId);
            return View();
        }

        // GET: UserItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserItems == null)
            {
                return NotFound();
            }

            var userItems = await _context.UserItems
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserItemsId == id);
            if (userItems == null)
            {
                return NotFound();
            }

            return View(userItems);
        }

        // POST: UserItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserItems'  is null.");
            }
            var userItems = await _context.UserItems.FindAsync(id);
            if (userItems != null)
            {
                _context.UserItems.Remove(userItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserItemsExists(int id)
        {
            return (_context.UserItems?.Any(e => e.UserItemsId == id)).GetValueOrDefault();
        }
    }
}
