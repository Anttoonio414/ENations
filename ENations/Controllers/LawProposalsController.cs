using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class LawProposalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LawProposalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LawProposals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LawProposals.Include(l => l.CongressMember);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LawProposals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LawProposals == null)
            {
                return NotFound();
            }

            var lawProposal = await _context.LawProposals
                .Include(l => l.CongressMember)
                .FirstOrDefaultAsync(m => m.LawProposalId == id);
            if (lawProposal == null)
            {
                return NotFound();
            }

            return View(lawProposal);
        }

        // GET: LawProposals/Create
        public IActionResult Create()
        {
            ViewData["CongressMemberId"] = new SelectList(_context.CongressMembers, "CongressMemberId", "CongressMemberId");
            return View();
        }

        // POST: LawProposals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LawProposalId,ExpectedVotes,Type,Reason,Amount,Yes,No,Finished,CongressMemberId")] int ExpectedVotes, string Type, string Reason, decimal Amount, bool Yes, bool No, bool Finished, int CongressMemberId)
        {
            if (ModelState.IsValid)
            {
                var lawProposal = new LawProposal();
                lawProposal.ExpectedVotes = ExpectedVotes;
                lawProposal.Type = Type;
                lawProposal.Reason = Reason;
                lawProposal.Amount = Amount;
                lawProposal.Yes = Yes;
                lawProposal.No = No;
                lawProposal.Finished = Finished;
                lawProposal.CongressMemberId = CongressMemberId;
                _context.Add(lawProposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CongressMemberId"] = new SelectList(_context.CongressMembers, "CongressMemberId", "CongressMemberId", lawProposal.CongressMemberId);
            return View();
        }

        // GET: LawProposals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LawProposals == null)
            {
                return NotFound();
            }

            var lawProposal = await _context.LawProposals.FindAsync(id);
            if (lawProposal == null)
            {
                return NotFound();
            }
            ViewData["CongressMemberId"] = new SelectList(_context.CongressMembers, "CongressMemberId", "CongressMemberId", lawProposal.CongressMemberId);
            return View(lawProposal);
        }

        // POST: LawProposals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LawProposalId,ExpectedVotes,Type,Reason,Amount,Yes,No,Finished,CongressMemberId")] int ExpectedVotes, string Type, string Reason, decimal Amount, bool Yes, bool No, bool Finished, int CongressMemberId)
        {


            if (ModelState.IsValid)
            {
                var lawProposal = await _context.LawProposals.FirstOrDefaultAsync(m => m.LawProposalId == id);
                lawProposal.ExpectedVotes = ExpectedVotes;
                lawProposal.Type = Type;
                lawProposal.Reason = Reason;
                lawProposal.Amount = Amount;
                lawProposal.Yes = Yes;
                lawProposal.No = No;
                lawProposal.Finished = Finished;
                lawProposal.CongressMemberId = CongressMemberId;
                _context.Update(lawProposal);

                return RedirectToAction(nameof(Index));
            }
            //ViewData["CongressMemberId"] = new SelectList(_context.CongressMembers, "CongressMemberId", "CongressMemberId", lawProposal.CongressMemberId);
            return View();
        }

        // GET: LawProposals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LawProposals == null)
            {
                return NotFound();
            }

            var lawProposal = await _context.LawProposals
                .Include(l => l.CongressMember)
                .FirstOrDefaultAsync(m => m.LawProposalId == id);
            if (lawProposal == null)
            {
                return NotFound();
            }

            return View(lawProposal);
        }

        // POST: LawProposals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LawProposals == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LawProposals'  is null.");
            }
            var lawProposal = await _context.LawProposals.FindAsync(id);
            if (lawProposal != null)
            {
                _context.LawProposals.Remove(lawProposal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LawProposalExists(int id)
        {
            return (_context.LawProposals?.Any(e => e.LawProposalId == id)).GetValueOrDefault();
        }
    }
}
