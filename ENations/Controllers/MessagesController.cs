using ENations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ENations.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Messages.Include(m => m.Chat).Include(m => m.Sender);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,Likes,ChatId,UserId")] int Likes, int ChatId, int UserId)
        {
            if (ModelState.IsValid)
            {
                var message = new Message();
                message.ChatId = ChatId;
                message.UserId = UserId;
                message.Likes = Likes;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId", message.ChatId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", message.UserId);
            return View();
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId", message.ChatId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", message.UserId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,Likes,ChatId,UserId")] int Likes, int ChatId, int UserId)
        {

            if (ModelState.IsValid)
            {
                var message = await _context.Messages.FirstOrDefaultAsync(m => m.MessageId == id);
                message.ChatId = ChatId;
                message.UserId = UserId;
                message.Likes = Likes;
                _context.Update(message);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId", message.ChatId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", message.UserId);
            return View();
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Messages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Messages'  is null.");
            }
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return (_context.Messages?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
    }
}
