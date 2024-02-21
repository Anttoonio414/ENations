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
            var applicationDbContext = _context.CongressMembers
                .Include(c => c.PartyMember)
                    .ThenInclude(p => p.User)
                .Include(c => c.PartyMember)
                    .ThenInclude(p => p.PoliticalParty); 
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CongressMembers/Create
        public IActionResult Create()
        {
            var partyMembers = _context.PartyMembers.Include(p => p.User).Select(pm => new
            {
                PartyMemberId = pm.PartyMemberId,
                Username = pm.User.Username
            }).ToList();

            ViewData["PartyMemberId"] = new SelectList(partyMembers, "PartyMemberId", "Username");
            return View();
        }

        // POST: CongressMembers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CongressMemberId,PartyMemberId")] int PartyMemberId)
        {
            if (ModelState.IsValid)
            {
                var congressMember = new CongressMember();
                congressMember.PartyMemberId = PartyMemberId;
                _context.Add(congressMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
                .Include(c => c.PartyMember)
                .ThenInclude(p => p.User)
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
