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
    public class PacijentisController : Controller
    {
        private readonly covidContext _context;

        public PacijentisController(covidContext context)
        {
            _context = context;
        }

        // GET: Pacijentis
        public async Task<IActionResult> Index()
        {
            var covidContext = _context.Pacijentis.Include(p => p.GradFkNavigation);
            return View(await covidContext.ToListAsync());
        }

        // GET: Pacijentis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijenti = await _context.Pacijentis
                .Include(p => p.GradFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacijenti == null)
            {
                return NotFound();
            }

            return View(pacijenti);
        }

        // GET: Pacijentis/Create
        public IActionResult Create()
        {
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv");
            return View();
        }

        // POST: Pacijentis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prezime,GradFk,DatumRodjenja,Ime,Email,Telefon")] Pacijenti pacijenti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacijenti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv", pacijenti.GradFk);
            return View(pacijenti);
        }

        // GET: Pacijentis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijenti = await _context.Pacijentis.FindAsync(id);
            if (pacijenti == null)
            {
                return NotFound();
            }
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv", pacijenti.GradFk);
            return View(pacijenti);
        }

        // POST: Pacijentis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Prezime,GradFk,DatumRodjenja,Ime,Email,Telefon")] Pacijenti pacijenti)
        {
            if (id != pacijenti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacijenti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacijentiExists(pacijenti.Id))
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
            ViewData["GradFk"] = new SelectList(_context.Gradovis, "Id", "Naziv", pacijenti.GradFk);
            return View(pacijenti);
        }

        // GET: Pacijentis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijenti = await _context.Pacijentis
                .Include(p => p.GradFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacijenti == null)
            {
                return NotFound();
            }

            return View(pacijenti);
        }

        // POST: Pacijentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pacijenti = await _context.Pacijentis.FindAsync(id);
            _context.Pacijentis.Remove(pacijenti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacijentiExists(long id)
        {
            return _context.Pacijentis.Any(e => e.Id == id);
        }
    }
}
