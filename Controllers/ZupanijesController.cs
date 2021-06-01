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
    public class ZupanijesController : Controller
    {
        private readonly covidContext _context;

        public ZupanijesController(covidContext context)
        {
            _context = context;
        }

        // GET: Zupanijes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zupanijes.ToListAsync());
        }

        // GET: Zupanijes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zupanije = await _context.Zupanijes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zupanije == null)
            {
                return NotFound();
            }

            return View(zupanije);
        }

        // GET: Zupanijes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zupanijes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrojStanovnika,Naziv")] Zupanije zupanije)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zupanije);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zupanije);
        }

        // GET: Zupanijes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zupanije = await _context.Zupanijes.FindAsync(id);
            if (zupanije == null)
            {
                return NotFound();
            }
            return View(zupanije);
        }

        // POST: Zupanijes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,BrojStanovnika,Naziv")] Zupanije zupanije)
        {
            if (id != zupanije.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zupanije);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZupanijeExists(zupanije.Id))
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
            return View(zupanije);
        }

        // GET: Zupanijes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zupanije = await _context.Zupanijes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zupanije == null)
            {
                return NotFound();
            }

            return View(zupanije);
        }

        // POST: Zupanijes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var zupanije = await _context.Zupanijes.FindAsync(id);
            _context.Zupanijes.Remove(zupanije);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZupanijeExists(long id)
        {
            return _context.Zupanijes.Any(e => e.Id == id);
        }
    }
}
