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
    public class UstanovesController : Controller
    {
        private readonly covidContext _context;

        public UstanovesController(covidContext context)
        {
            _context = context;
        }

        // GET: Ustanoves
        public async Task<IActionResult> Index()
        {
            var covidContext = _context.Ustanoves.Include(u => u.GradFkNavigation);
            return View(await covidContext.ToListAsync());
        }

        // GET: Ustanoves/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ustanove = await _context.Ustanoves
                .Include(u => u.GradFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ustanove == null)
            {
                return NotFound();
            }

            return View(ustanove);
        }

        // GET: Ustanoves/Create
        public IActionResult Create()
        {
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv");
            return View();
        }

        // POST: Ustanoves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GradFk,Naziv")] Ustanove ustanove)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ustanove);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv", ustanove.GradFk);
            return View(ustanove);
        }

        // GET: Ustanoves/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ustanove = await _context.Ustanoves.FindAsync(id);
            if (ustanove == null)
            {
                return NotFound();
            }
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv", ustanove.GradFk);
            return View(ustanove);
        }

        // POST: Ustanoves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,GradFk,Naziv")] Ustanove ustanove)
        {
            if (id != ustanove.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ustanove);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UstanoveExists(ustanove.Id))
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
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv", ustanove.GradFk);
            return View(ustanove);
        }

        // GET: Ustanoves/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ustanove = await _context.Ustanoves
                .Include(u => u.GradFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ustanove == null)
            {
                return NotFound();
            }

            return View(ustanove);
        }

        // POST: Ustanoves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var ustanove = await _context.Ustanoves.FindAsync(id);
            _context.Ustanoves.Remove(ustanove);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UstanoveExists(long id)
        {
            return _context.Ustanoves.Any(e => e.Id == id);
        }
    }
}
