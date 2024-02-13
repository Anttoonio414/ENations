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
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Articles.Include(a => a.Newspaper);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var article = await _context.Articles
                .Include(a => a.Newspaper)
                .FirstOrDefaultAsync(m => m.ArticleId == id);

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["NewspaperId"] = new SelectList(_context.Newspapers, "NewspaperId", "Name");
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,Votes,Views,Category,Text,Title,NewspaperId")] int Votes, int Views, string Category, string Text, string Title, int NewspaperId)
        {
            if (ModelState.IsValid)
            {
                var article = new Article();
                article.Votes = Votes;
                article.Views = Views;
                article.Category = Category;
                article.Text = Text;
                article.Title = Title;
                article.NewspaperId = NewspaperId;
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var article = await _context.Articles.FindAsync(id);

            ViewData["NewspaperId"] = new SelectList(_context.Newspapers, "NewspaperId", "Name", article.NewspaperId);
            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Votes,Views,Category,Text,Title,NewspaperId")] int Votes, int Views, string Category, string Text, string Title, int NewspaperId)
        {
            if (ModelState.IsValid)
            {
                var article = await _context.Articles.FirstOrDefaultAsync(m => m.ArticleId == id);
                article.Votes = Votes;
                article.Views = Views;
                article.Category = Category;
                article.Text = Text;
                article.Title = Title;
                article.NewspaperId = NewspaperId;
                _context.Update(article);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var article = await _context.Articles
                .Include(a => a.Newspaper)
                .FirstOrDefaultAsync(m => m.ArticleId == id);

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return (_context.Articles?.Any(e => e.ArticleId == id)).GetValueOrDefault();
        }
    }
}
