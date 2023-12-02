using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication1.Controllers
{
    public class ReadersController : Controller
    {
        private readonly ReadersDbContext _context;

        public ReadersController(ReadersDbContext context)
        {
            _context = context;
        }

        // GET: Readers
        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                // Проверка за текущия ред на сортиране
                bool isSortOrderDesc = string.Equals(sortOrder, "name_desc", StringComparison.OrdinalIgnoreCase);

                // Превключване на реда на сортиране при повторно натискане
                ViewBag.NameSortParam = isSortOrderDesc ? "name_asc" : "name_desc";

                var readers = _context.Readers.ToList();

                // Използвайте новата променлива за проверка на реда на сортиране
                switch (ViewBag.NameSortParam)
                {
                    case "name_desc":
                        readers = readers.OrderByDescending(b => b.Name).ToList();
                        break;
                    default:
                        readers = readers.OrderBy(b => b.Name).ToList();
                        break;
                }

                return View(readers);
            }
            catch (Exception ex)
            {
                return _context.Readers != null ?
                          View(await _context.Readers.ToListAsync()) :
                          Problem("Entity set 'BooksDbContext.Books'  is null.");
            }
        }

            // GET: Readers/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var readers = await _context.Readers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readers == null)
            {
                return NotFound();
            }

            return View(readers);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,FavoriteGenres")] Readers readers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(readers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(readers);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var readers = await _context.Readers.FindAsync(id);
            if (readers == null)
            {
                return NotFound();
            }
            return View(readers);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,FavoriteGenres")] Readers readers)
        {
            if (id != readers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(readers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadersExists(readers.Id))
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
            return View(readers);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var readers = await _context.Readers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readers == null)
            {
                return NotFound();
            }

            return View(readers);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Readers == null)
            {
                return Problem("Entity set 'ReadersDbContext.Readers'  is null.");
            }
            var readers = await _context.Readers.FindAsync(id);
            if (readers != null)
            {
                _context.Readers.Remove(readers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReadersExists(int id)
        {
          return (_context.Readers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
