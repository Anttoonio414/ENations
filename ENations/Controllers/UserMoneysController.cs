using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class UserMoneysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserMoneysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserMoneys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserMoney.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserMoneys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserMoney == null)
            {
                return NotFound();
            }

            var userMoney = await _context.UserMoney
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userMoney == null)
            {
                return NotFound();
            }

            return View(userMoney);
        }

        // GET: UserMoneys/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: UserMoneys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Gold,Currency")] int UserId, decimal Gold, string Currency)
        {
            if (ModelState.IsValid)
            {
                var userMoney = new UserMoney();
                userMoney.UserId = UserId;
                userMoney.Gold = Gold;
                userMoney.Currency = Currency;
                _context.Add(userMoney);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userMoney.UserId);
            return View();
        }

        // GET: UserMoneys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserMoney == null)
            {
                return NotFound();
            }

            var userMoney = await _context.UserMoney.FindAsync(id);
            if (userMoney == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", userMoney.UserId);
            return View(userMoney);
        }

        // POST: UserMoneys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Gold,Currency")] int UserId, decimal Gold, string Currency)
        {

            if (ModelState.IsValid)
            {
                var userMoney = await _context.UserMoney.FirstOrDefaultAsync(m => m.UserId == id);
                userMoney.UserId = UserId;
                userMoney.Gold = Gold;
                userMoney.Currency = Currency;
                _context.Update(userMoney);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userMoney.UserId);
            return View();
        }

        // GET: UserMoneys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserMoney == null)
            {
                return NotFound();
            }

            var userMoney = await _context.UserMoney
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userMoney == null)
            {
                return NotFound();
            }

            return View(userMoney);
        }

        // POST: UserMoneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserMoney == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserMoney'  is null.");
            }
            var userMoney = await _context.UserMoney.FindAsync(id);
            if (userMoney != null)
            {
                _context.UserMoney.Remove(userMoney);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMoneyExists(int id)
        {
            return (_context.UserMoney?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
