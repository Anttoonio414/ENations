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
    public class ChatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chats
        public async Task<IActionResult> Index()
        {
            return _context.Chats != null ?
                        View(await _context.Chats.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Chats'  is null.");
        }

        // GET: Chats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chats == null)
            {
                return NotFound();
            }

            var chat = await _context.Chats
                .FirstOrDefaultAsync(m => m.ChatId == id);
            if (chat == null)
            {
                return NotFound();
            }

            return View(chat);
        }

        // GET: Chats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChatId,ChannelType,ChannelId")] string ChannelType, string ChannelId)
        {
            if (ModelState.IsValid)
            {
                var chat = new Chat();
                chat.ChannelType = ChannelType;
                chat.ChannelId = ChannelId;
                _context.Add(chat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Chats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var chat = await _context.Chats.FindAsync(id);
            return View(chat);
        }

        // POST: Chats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChatId,ChannelType,ChannelId")] string ChannelType, string ChannelId)
        {
            if (ModelState.IsValid)
            {
                var chat = await _context.Chats
                    .FirstOrDefaultAsync(m => m.ChatId.Equals(id));
                chat.ChannelType = ChannelType;
                chat.ChannelId = ChannelId;
                _context.Update(chat);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Chats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var chat = await _context.Chats
                .FirstOrDefaultAsync(m => m.ChatId == id);

            return View(chat);
        }

        // POST: Chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat != null)
            {
                _context.Chats.Remove(chat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatExists(int id)
        {
            return (_context.Chats?.Any(e => e.ChatId == id)).GetValueOrDefault();
        }
    }
}
