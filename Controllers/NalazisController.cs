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
    public class NalazisController : Controller
    {
        private readonly covidContext _context;

        public NalazisController(covidContext context)
        {
            _context = context;
        }

        // GET: Nalazis
        public async Task<IActionResult> Index()
        {
            var covidContext = _context.Nalazis.Include(n => n.PacijentFkNavigation).Include(n => n.TestFkNavigation).Include(n => n.UplataFkNavigation).Include(n => n.UstanovaFkNavigation);
            return View(await covidContext.ToListAsync());
        }

        // GET: Nalazis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nalazi = await _context.Nalazis
                .Include(n => n.PacijentFkNavigation)
                .Include(n => n.TestFkNavigation)
                .Include(n => n.UplataFkNavigation)
                .Include(n => n.UstanovaFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nalazi == null)
            {
                return NotFound();
            }

            return View(nalazi);
        }

        // GET: Nalazis/Create
        public IActionResult Create()
        {
            ViewData["PacijentFk"] = new SelectList(_context.Pacijentis, "Id", "FullName");
            ViewData["TestFk"] = new SelectList(_context.Testovis, "Id", "Naziv");
            ViewData["UplataFk"] = new SelectList(_context.Uplates, "Id", "Vrsta");
            ViewData["UstanovaFk"] = new SelectList(_context.Ustanoves, "Id", "Naziv");
            return View();
        }

        // POST: Nalazis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestFk,Datum,UstanovaFk,PacijentFk,UplataFk,Rezultat")] Nalazi nalazi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nalazi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacijentFk"] = new SelectList(_context.Pacijentis, "Id", "FullName", nalazi.PacijentFk);
            ViewData["TestFk"] = new SelectList(_context.Testovis, "Id", "Naziv", nalazi.TestFk);
            ViewData["UplataFk"] = new SelectList(_context.Uplates, "Id", "Vrsta", nalazi.UplataFk);
            ViewData["UstanovaFk"] = new SelectList(_context.Ustanoves, "Id", "Naziv", nalazi.UstanovaFk);
            return View(nalazi);
        }

        // GET: Nalazis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nalazi = await _context.Nalazis.FindAsync(id);
            if (nalazi == null)
            {
                return NotFound();
            }
            ViewData["PacijentFk"] = new SelectList(_context.Pacijentis, "Id", "FullName", nalazi.PacijentFk);
            ViewData["TestFk"] = new SelectList(_context.Testovis, "Id", "Naziv", nalazi.TestFk);
            ViewData["UplataFk"] = new SelectList(_context.Uplates, "Id", "Vrsta", nalazi.UplataFk);
            ViewData["UstanovaFk"] = new SelectList(_context.Ustanoves, "Id", "Naziv", nalazi.UstanovaFk);
            return View(nalazi);
        }

        // POST: Nalazis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,TestFk,Datum,UstanovaFk,PacijentFk,UplataFk,Rezultat")] Nalazi nalazi)
        {
            if (id != nalazi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nalazi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NalaziExists(nalazi.Id))
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
            ViewData["PacijentFk"] = new SelectList(_context.Pacijentis, "Id", "FullName", nalazi.PacijentFk);
            ViewData["TestFk"] = new SelectList(_context.Testovis, "Id", "Naziv", nalazi.TestFk);
            ViewData["UplataFk"] = new SelectList(_context.Uplates, "Id", "Vrsta", nalazi.UplataFk);
            ViewData["UstanovaFk"] = new SelectList(_context.Ustanoves, "Id", "Naziv", nalazi.UstanovaFk);
            return View(nalazi);
        }

        // GET: Nalazis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nalazi = await _context.Nalazis
                .Include(n => n.PacijentFkNavigation)
                .Include(n => n.TestFkNavigation)
                .Include(n => n.UplataFkNavigation)
                .Include(n => n.UstanovaFkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nalazi == null)
            {
                return NotFound();
            }

            return View(nalazi);
        }

        // POST: Nalazis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nalazi = await _context.Nalazis.FindAsync(id);
            _context.Nalazis.Remove(nalazi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NalaziExists(long id)
        {
            return _context.Nalazis.Any(e => e.Id == id);
        }
    }
}
