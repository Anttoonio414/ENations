using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class PartyMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartyMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PartyMembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PartyMembers.Include(p => p.PoliticalParty).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PartyMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PartyMembers == null)
            {
                return NotFound();
            }

            var partyMember = await _context.PartyMembers
                .Include(p => p.PoliticalParty)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PartyMemberId == id);
            if (partyMember == null)
            {
                return NotFound();
            }

            return View(partyMember);
        }

        // GET: PartyMembers/Create
        public IActionResult Create()
        {
            ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: PartyMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartyMemberId,Level,PoliticalPartyId,UserId")] int Level, int PoliticalPartyId, int UserId)
        {
            if (ModelState.IsValid)
            {
                var partyMember = new PartyMember();
                partyMember.Level = Level;
                partyMember.UserId = UserId;
                partyMember.PoliticalPartyId = PoliticalPartyId;
                _context.Add(partyMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "PoliticalPartyId", partyMember.PoliticalPartyId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", partyMember.UserId);
            return View();
        }

        // GET: PartyMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PartyMembers == null)
            {
                return NotFound();
            }

            var partyMember = await _context.PartyMembers.FindAsync(id);
            if (partyMember == null)
            {
                return NotFound();
            }
            ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "Name", partyMember.PoliticalPartyId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", partyMember.UserId);
            return View(partyMember);
        }

        // POST: PartyMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartyMemberId,Level,PoliticalPartyId,UserId")] int Level, int PoliticalPartyId, int UserId)
        {

            if (ModelState.IsValid)
            {
                var partyMember = await _context.PartyMembers.FirstOrDefaultAsync(m => m.PartyMemberId == id);
                partyMember.Level = Level;
                partyMember.UserId = UserId;
                partyMember.PoliticalPartyId = PoliticalPartyId;
                _context.Update(partyMember);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["PoliticalPartyId"] = new SelectList(_context.PoliticalParties, "PoliticalPartyId", "PoliticalPartyId", partyMember.PoliticalPartyId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", partyMember.UserId);
            return View();
        }

        // GET: PartyMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PartyMembers == null)
            {
                return NotFound();
            }

            var partyMember = await _context.PartyMembers
                .Include(p => p.PoliticalParty)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PartyMemberId == id);
            if (partyMember == null)
            {
                return NotFound();
            }

            return View(partyMember);
        }

        // POST: PartyMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PartyMembers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PartyMembers'  is null.");
            }
            var partyMember = await _context.PartyMembers.FindAsync(id);
            if (partyMember != null)
            {
                _context.PartyMembers.Remove(partyMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartyMemberExists(int id)
        {
            return (_context.PartyMembers?.Any(e => e.PartyMemberId == id)).GetValueOrDefault();
        }
    }
}
