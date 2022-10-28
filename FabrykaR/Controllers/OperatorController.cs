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
    public class OperatorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperatorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Operator
        public async Task<IActionResult> Index()
        {
              return View(await _context.OperatorSet.ToListAsync());
        }

        // GET: Operator/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OperatorSet == null)
            {
                return NotFound();
            }

            var @operator = await _context.OperatorSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@operator == null)
            {
                return NotFound();
            }

            return View(@operator);
        }

        // GET: Operator/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwisko,Imie,Placa")] Operator @operator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@operator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@operator);
        }

        // GET: Operator/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OperatorSet == null)
            {
                return NotFound();
            }

            var @operator = await _context.OperatorSet.FindAsync(id);
            if (@operator == null)
            {
                return NotFound();
            }
            return View(@operator);
        }

        // POST: Operator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwisko,Imie,Placa")] Operator @operator)
        {
            if (id != @operator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@operator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperatorExists(@operator.Id))
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
            return View(@operator);
        }

        // GET: Operator/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OperatorSet == null)
            {
                return NotFound();
            }

            var @operator = await _context.OperatorSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@operator == null)
            {
                return NotFound();
            }

            return View(@operator);
        }

        // POST: Operator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OperatorSet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OperatorSet'  is null.");
            }
            var @operator = await _context.OperatorSet.FindAsync(id);
            if (@operator != null)
            {
                _context.OperatorSet.Remove(@operator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperatorExists(int id)
        {
          return _context.OperatorSet.Any(e => e.Id == id);
        }
    }
}
