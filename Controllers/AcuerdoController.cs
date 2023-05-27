using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using amazon.Models;

namespace amazon.Controllers
{
    public class AcuerdoController : Controller
    {
        private readonly DbamazonContext _context;

        public AcuerdoController(DbamazonContext context)
        {
            _context = context;
        }

        // GET: Acuerdo
        public async Task<IActionResult> Index()
        {
            var dbamazonContext = _context.Acuerdos.Include(a => a.Pais);
            return View(await dbamazonContext.ToListAsync());
        }

        // GET: Acuerdo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Acuerdos == null)
            {
                return NotFound();
            }

            var acuerdo = await _context.Acuerdos
                .Include(a => a.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acuerdo == null)
            {
                return NotFound();
            }

            return View(acuerdo);
        }

        // GET: Acuerdo/Create
        public IActionResult Create()
        {
            ViewData["Paisid"] = new SelectList(_context.Paises, "Id", "Id");
            return View();
        }

        // POST: Acuerdo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Contenido,Nombre,Tipo,Paisid")] Acuerdo acuerdo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acuerdo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Paisid"] = new SelectList(_context.Paises, "Id", "Id", acuerdo.Paisid);
            return View(acuerdo);
        }

        // GET: Acuerdo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Acuerdos == null)
            {
                return NotFound();
            }

            var acuerdo = await _context.Acuerdos.FindAsync(id);
            if (acuerdo == null)
            {
                return NotFound();
            }
            ViewData["Paisid"] = new SelectList(_context.Paises, "Id", "Id", acuerdo.Paisid);
            return View(acuerdo);
        }

        // POST: Acuerdo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Contenido,Nombre,Tipo,Paisid")] Acuerdo acuerdo)
        {
            if (id != acuerdo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acuerdo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcuerdoExists(acuerdo.Id))
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
            ViewData["Paisid"] = new SelectList(_context.Paises, "Id", "Id", acuerdo.Paisid);
            return View(acuerdo);
        }

        // GET: Acuerdo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Acuerdos == null)
            {
                return NotFound();
            }

            var acuerdo = await _context.Acuerdos
                .Include(a => a.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acuerdo == null)
            {
                return NotFound();
            }

            return View(acuerdo);
        }

        // POST: Acuerdo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Acuerdos == null)
            {
                return Problem("Entity set 'DbamazonContext.Acuerdos'  is null.");
            }
            var acuerdo = await _context.Acuerdos.FindAsync(id);
            if (acuerdo != null)
            {
                _context.Acuerdos.Remove(acuerdo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcuerdoExists(int id)
        {
          return (_context.Acuerdos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
