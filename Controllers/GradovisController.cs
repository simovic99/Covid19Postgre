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
    public class GradovisController : Controller
    {
        private readonly covidContext _context;

        public GradovisController(covidContext context)
        {
            _context = context;
        }

        // GET: Gradovis
        public async Task<IActionResult> Index()
        {
            var covidContext = _context.Gradovis.Include(g => g.FkZupanijaNavigation);
            return View(await covidContext.ToListAsync());
        }

        // GET: Gradovis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradovi = await _context.Gradovis
                .Include(g => g.FkZupanijaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradovi == null)
            {
                return NotFound();
            }

            return View(gradovi);
        }

        // GET: Gradovis/Create
        public IActionResult Create()
        {
            ViewData["FkZupanija"] = new SelectList(_context.Zupanijes, "Id", "Naziv");
            return View();
        }

        // POST: Gradovis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FkZupanija,BrojStanovnika,Naziv")] Gradovi gradovi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gradovi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkZupanija"] = new SelectList(_context.Zupanijes, "Id", "Naziv", gradovi.FkZupanija);
            return View(gradovi);
        }

        // GET: Gradovis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradovi = await _context.Gradovis.FindAsync(id);
            if (gradovi == null)
            {
                return NotFound();
            }
            ViewData["FkZupanija"] = new SelectList(_context.Zupanijes, "Id", "Naziv", gradovi.FkZupanija);
            return View(gradovi);
        }

        // POST: Gradovis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FkZupanija,BrojStanovnika,Naziv")] Gradovi gradovi)
        {
            if (id != gradovi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gradovi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradoviExists(gradovi.Id))
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
            ViewData["FkZupanija"] = new SelectList(_context.Zupanijes, "Id", "Naziv", gradovi.FkZupanija);
            return View(gradovi);
        }

        // GET: Gradovis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gradovi = await _context.Gradovis
                .Include(g => g.FkZupanijaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gradovi == null)
            {
                return NotFound();
            }

            return View(gradovi);
        }

        // POST: Gradovis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var gradovi = await _context.Gradovis.FindAsync(id);
            _context.Gradovis.Remove(gradovi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradoviExists(long id)
        {
            return _context.Gradovis.Any(e => e.Id == id);
        }
    }
}
