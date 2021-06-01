using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5;

namespace WebApplication5.Controllers
{
    public class TestovisController : Controller
    {
        private readonly covidContext _context;

        public TestovisController(covidContext context)
        {
            _context = context;
        }

        // GET: Testovis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Testovis.ToListAsync());
        }

        // GET: Testovis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testovi = await _context.Testovis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testovi == null)
            {
                return NotFound();
            }

            return View(testovi);
        }

        // GET: Testovis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Testovis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cijena,Naziv")] Testovi testovi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testovi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testovi);
        }

        // GET: Testovis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testovi = await _context.Testovis.FindAsync(id);
            if (testovi == null)
            {
                return NotFound();
            }
            return View(testovi);
        }

        // POST: Testovis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Cijena,Naziv")] Testovi testovi)
        {
            if (id != testovi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testovi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestoviExists(testovi.Id))
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
            return View(testovi);
        }

        // GET: Testovis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testovi = await _context.Testovis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testovi == null)
            {
                return NotFound();
            }

            return View(testovi);
        }

        // POST: Testovis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var testovi = await _context.Testovis.FindAsync(id);
            _context.Testovis.Remove(testovi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestoviExists(long id)
        {
            return _context.Testovis.Any(e => e.Id == id);
        }
    }
}
