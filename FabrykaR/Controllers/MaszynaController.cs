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
    public class MaszynaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaszynaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Maszyna
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var maszynyDbContext = _context.MaszynaSet.Include(m => m.Hala).OrderBy(m => m.Nazwa);
                return View(await maszynyDbContext.ToListAsync());
            }
            else
            {
                var maszynyDbContext = _context.MaszynaSet.Include(m => m.Hala).Where(m => m.Nazwa.Contains(search)).OrderBy(m => m.Nazwa);
                return View(await maszynyDbContext.ToListAsync());
            }
        }

        // GET: Maszyna/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaszynaSet == null)
            {
                return NotFound();
            }

            var maszyna = await _context.MaszynaSet
                .Include(m => m.Hala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maszyna == null)
            {
                return NotFound();
            }

            return View(maszyna);
        }

        // GET: Maszyna/Create
        public IActionResult Create()
        {
            ViewData["HalaId"] = new SelectList(_context.HalaSet, "Id", "Nazwa");
            return View();
        }

        // POST: Maszyna/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Data_uruchomienia,HalaId")] Maszyna maszyna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maszyna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HalaId"] = new SelectList(_context.HalaSet, "Id", "Id", maszyna.HalaId);
            return View(maszyna);
        }

        // GET: Maszyna/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaszynaSet == null)
            {
                return NotFound();
            }

            var maszyna = await _context.MaszynaSet.FindAsync(id);
            if (maszyna == null)
            {
                return NotFound();
            }
            ViewData["HalaId"] = new SelectList(_context.HalaSet, "Id", "Id", maszyna.HalaId);
            return View(maszyna);
        }

        // POST: Maszyna/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Data_uruchomienia,HalaId")] Maszyna maszyna)
        {
            if (id != maszyna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maszyna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaszynaExists(maszyna.Id))
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
            ViewData["HalaId"] = new SelectList(_context.HalaSet, "Id", "Id", maszyna.HalaId);
            return View(maszyna);
        }

        // GET: Maszyna/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaszynaSet == null)
            {
                return NotFound();
            }

            var maszyna = await _context.MaszynaSet
                .Include(m => m.Hala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maszyna == null)
            {
                return NotFound();
            }

            return View(maszyna);
        }

        // POST: Maszyna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaszynaSet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MaszynaSet'  is null.");
            }
            var maszyna = await _context.MaszynaSet.FindAsync(id);
            if (maszyna != null)
            {
                _context.MaszynaSet.Remove(maszyna);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaszynaExists(int id)
        {
          return _context.MaszynaSet.Any(e => e.Id == id);
        }
    }
}
