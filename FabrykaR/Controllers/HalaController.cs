using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FabrykaR.Data;
using FabrykaR.Models;

namespace FabrykaR.Controllers
{
    public class HalaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HalaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hala
        public async Task<IActionResult> Index()
        {
              return View(await _context.HalaSet.ToListAsync());
        }

        // GET: Hala/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HalaSet == null)
            {
                return NotFound();
            }

            var hala = await _context.HalaSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hala == null)
            {
                return NotFound();
            }

            return View(hala);
        }

        // GET: Hala/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Adres")] Hala hala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hala);
        }

        // GET: Hala/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HalaSet == null)
            {
                return NotFound();
            }

            var hala = await _context.HalaSet.FindAsync(id);
            if (hala == null)
            {
                return NotFound();
            }
            return View(hala);
        }

        // POST: Hala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Adres")] Hala hala)
        {
            if (id != hala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HalaExists(hala.Id))
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
            return View(hala);
        }

        // GET: Hala/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HalaSet == null)
            {
                return NotFound();
            }

            var hala = await _context.HalaSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hala == null)
            {
                return NotFound();
            }

            return View(hala);
        }

        // POST: Hala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HalaSet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HalaSet'  is null.");
            }
            var hala = await _context.HalaSet.FindAsync(id);
            if (hala != null)
            {
                _context.HalaSet.Remove(hala);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HalaExists(int id)
        {
          return _context.HalaSet.Any(e => e.Id == id);
        }
    }
}
