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
    public class UplatesController : Controller
    {
        private readonly covidContext _context;

        public UplatesController(covidContext context)
        {
            _context = context;
        }

        // GET: Uplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uplates.ToListAsync());
        }

        // GET: Uplates/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uplate = await _context.Uplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uplate == null)
            {
                return NotFound();
            }

            return View(uplate);
        }

        // GET: Uplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Iznos,Vrsta")] Uplate uplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uplate);
        }

        // GET: Uplates/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uplate = await _context.Uplates.FindAsync(id);
            if (uplate == null)
            {
                return NotFound();
            }
            return View(uplate);
        }

        // POST: Uplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Iznos,Vrsta")] Uplate uplate)
        {
            if (id != uplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UplateExists(uplate.Id))
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
            return View(uplate);
        }

        // GET: Uplates/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uplate = await _context.Uplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uplate == null)
            {
                return NotFound();
            }

            return View(uplate);
        }

        // POST: Uplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var uplate = await _context.Uplates.FindAsync(id);
            _context.Uplates.Remove(uplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UplateExists(long id)
        {
            return _context.Uplates.Any(e => e.Id == id);
        }
    }
}
