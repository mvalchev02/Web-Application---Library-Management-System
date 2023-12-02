using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sitecore.FakeDb;
using WebApplication1.Models;
using PagedList;
using X.PagedList;

namespace WebApplication1.Controllers
{
    public class ReaderBooksController : Controller
    {
        private readonly ReaderBookDbContext _context;

        public ReaderBooksController(ReaderBookDbContext context)
        {
            _context = context;
        }

        // GET: ReaderBooks
        public async Task<IActionResult> Index(string sortOrder, string readerName, string genre, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.BookGetSortParam = sortOrder == "BookGet" ? "bookGet_desc" : "BookGet";
            ViewBag.BookReturnSortParam = sortOrder == "BookReturn" ? "bookReturn_desc" : "BookReturn";

            if (readerName != null && genre != null)
            {
                page = 1;
            }
            else
            {
                readerName = currentFilter;
            }

            ViewBag.CurrentFilter = readerName;

            var readerBookDbContext = _context.ReaderBook.AsQueryable();

            // Add search conditions if parameters are provided
            if (!string.IsNullOrEmpty(readerName))
            {
                readerBookDbContext = readerBookDbContext.Where(rb => rb.Lecteur.Name == readerName);
            }

            if (!string.IsNullOrEmpty(genre))
            {
                readerBookDbContext = readerBookDbContext.Where(rc => rc.Livre.Genre == genre);
            }

            switch (sortOrder)
            {
                case "BookGet":
                    readerBookDbContext = readerBookDbContext.OrderBy(s => s.BookGet);
                    break;
                case "BookReturn":
                    readerBookDbContext = readerBookDbContext.OrderBy(s => s.BookReturn);
                    break;
                case "bookReturn_desc":
                    readerBookDbContext = readerBookDbContext.OrderByDescending(s => s.BookReturn);
                    break;
                default:
                    readerBookDbContext = readerBookDbContext.OrderByDescending(s => s.BookGet);
                    break;
            }

            var pageNumber = page ?? 1;
            var pageSize = 3;

            var bookGett = await readerBookDbContext
                .Include(rb => rb.Lecteur)
                .Include(rb => rb.Livre)
                .ToPagedListAsync(pageNumber, pageSize);

            return View(bookGett);
        }


        // GET: ReaderBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReaderBook == null)
            {
                return NotFound();
            }

            var readerBook = await _context.ReaderBook
                .Include(r => r.Lecteur)
                .Include(b => b.Livre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readerBook == null)
            {
                return NotFound();
            }

            return View(readerBook);
        }

        // GET: ReaderBooks/Create
        public IActionResult Create()
        {
            ViewData["LecteurId"] = new SelectList(_context.Set<Readers>(), "Id", "Name");
            ViewData["LivreId"] = new SelectList(_context.Set<Books>(), "Id", "Title");
            return View();
        }

        // POST: ReaderBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LivreId,LecteurId,BookGet,BookReturn")] ReaderBook readerBook)
        {           
            if (ModelState.IsValid)
            {
                _context.Add(readerBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LecteurId"] = new SelectList(_context.Set<Readers>(), "Id", "Name", readerBook.LecteurId);
            ViewData["LivreId"] = new SelectList(_context.Set<Books>(), "Id", "Title", readerBook.LivreId);

            return View(readerBook);
        }

        // GET: ReaderBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReaderBook == null)
            {
                return NotFound();
            }

            var readerBook = await _context.ReaderBook.FindAsync(id);
            if (readerBook == null)
            {
                return NotFound();
            }
            ViewData["LecteurId"] = new SelectList(_context.Set<Readers>(), "Id", "Name", readerBook.LecteurId);
            ViewData["LivreId"] = new SelectList(_context.Set<Books>(), "Id", "Title", readerBook.LivreId);

            return View(readerBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LivreId,LecteurId,BookGet,BookReturn")] ReaderBook readerBook)
        {
            if (id != readerBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(readerBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaderBookExists(readerBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecteurId"] = new SelectList(_context.Set<Readers>(), "Id", "Name", readerBook.LecteurId);
            ViewData["LivreId"] = new SelectList(_context.Set<Books>(), "Id", "Title", readerBook.LivreId);

            return View(readerBook);
        }

        // GET: ReaderBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReaderBook == null)
            {
                return NotFound();
            }

            var readerBook = await _context.ReaderBook
                .Include(r => r.Lecteur)
                .Include(b => b.Livre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readerBook == null)
            {
                return NotFound();
            }

            return View(readerBook);
        }

        // POST: ReaderBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReaderBook == null)
            {
                return Problem("Entity set 'ReaderBookDbContext.ReaderBook'  is null.");
            }
            var readerBook = await _context.ReaderBook.FindAsync(id);
            if (readerBook != null)
            {
                _context.ReaderBook.Remove(readerBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaderBookExists(int id)
        {
          return (_context.ReaderBook?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
