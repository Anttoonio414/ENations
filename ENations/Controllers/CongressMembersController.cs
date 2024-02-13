using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class CongressMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CongressMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CongressMembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CongressMembers.Include(c => c.PoliticalParty);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CongressMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CongressMembers == null)
            {
                return NotFound();
            }

            var congressMember = await _context.CongressMembers
                .Include(c => c.PoliticalParty)
                .FirstOrDefaultAsync(m => m.CongressMemberId == id);
            if (congressMember == null)
            {
                return NotFound();
            }

            return View(congressMember);
        }

        // GET: CongressMembers/Create
        public IActionResult Create()
        {
            ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "PoliticalPartyId");
            return View();
        }

        // POST: CongressMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CongressMemberId,PoliticalPartyId")] int PoliticalPartyId)
        {
            if (ModelState.IsValid)
            {
                var congressMember = new CongressMember();
                congressMember.PoliticalPartyId = PoliticalPartyId;
                _context.Add(congressMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "PoliticalPartyId", congressMember.PoliticalPartyId);
            return View();
        }

        // GET: CongressMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CongressMembers == null)
            {
                return NotFound();
            }

            var congressMember = await _context.CongressMembers.FindAsync(id);
            if (congressMember == null)
            {
                return NotFound();
            }
            ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "PoliticalPartyId", congressMember.PoliticalPartyId);
            return View(congressMember);
        }

        // POST: CongressMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CongressMemberId,PoliticalPartyId")] int PoliticalPartyId)
        {


            if (ModelState.IsValid)
            {
                var congressMember = await _context.CongressMembers
                    .FirstOrDefaultAsync(m => m.CongressMemberId == id);
                congressMember.PoliticalPartyId = PoliticalPartyId;
                _context.Update(congressMember);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "PoliticalPartyId", congressMember.PoliticalPartyId);
            return View();
        }

        // GET: CongressMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CongressMembers == null)
            {
                return NotFound();
            }

            var congressMember = await _context.CongressMembers
                .Include(c => c.PoliticalParty)
                .FirstOrDefaultAsync(m => m.CongressMemberId == id);
            if (congressMember == null)
            {
                return NotFound();
            }

            return View(congressMember);
        }

        // POST: CongressMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CongressMembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CongressMembers'  is null.");
            }
            var congressMember = await _context.CongressMembers.FindAsync(id);
            if (congressMember != null)
            {
                _context.CongressMembers.Remove(congressMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongressMemberExists(int id)
        {
            return (_context.CongressMembers?.Any(e => e.CongressMemberId == id)).GetValueOrDefault();
        }
    }
}
