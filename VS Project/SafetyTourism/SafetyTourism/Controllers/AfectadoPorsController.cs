using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafetyTourism.Data;
using SafetyTourism.Models;

namespace SafetyTourism.Controllers
{
    public class AfectadoPorsController : Controller
    {
        private readonly SafetyContext _context;

        public AfectadoPorsController(SafetyContext context)
        {
            _context = context;
        }

        // GET: AfectadoPors
        public async Task<IActionResult> Index()
        {
            var safetyContext = _context.Afectados.Include(a => a.destino).Include(a => a.doenca);
            return View(await safetyContext.ToListAsync());
        }

        // GET: AfectadoPors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afectadoPor = await _context.Afectados
                .Include(a => a.destino)
                .Include(a => a.doenca)
                .FirstOrDefaultAsync(m => m.AfectadoPorId == id);
            if (afectadoPor == null)
            {
                return NotFound();
            }

            return View(afectadoPor);
        }

        // GET: AfectadoPors/Create
        public IActionResult Create()
        {
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId");
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId");
            return View();
        }

        // POST: AfectadoPors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AfectadoPorId,Data,Gravidade,InfectadosPor100k,DestinoId,DoencaId")] AfectadoPor afectadoPor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(afectadoPor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", afectadoPor.DestinoId);
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId", afectadoPor.DoencaId);
            return View(afectadoPor);
        }

        // GET: AfectadoPors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afectadoPor = await _context.Afectados.FindAsync(id);
            if (afectadoPor == null)
            {
                return NotFound();
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", afectadoPor.DestinoId);
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId", afectadoPor.DoencaId);
            return View(afectadoPor);
        }

        // POST: AfectadoPors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AfectadoPorId,Data,Gravidade,InfectadosPor100k,DestinoId,DoencaId")] AfectadoPor afectadoPor)
        {
            if (id != afectadoPor.AfectadoPorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(afectadoPor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfectadoPorExists(afectadoPor.AfectadoPorId))
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
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", afectadoPor.DestinoId);
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId", afectadoPor.DoencaId);
            return View(afectadoPor);
        }

        // GET: AfectadoPors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afectadoPor = await _context.Afectados
                .Include(a => a.destino)
                .Include(a => a.doenca)
                .FirstOrDefaultAsync(m => m.AfectadoPorId == id);
            if (afectadoPor == null)
            {
                return NotFound();
            }

            return View(afectadoPor);
        }

        // POST: AfectadoPors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afectadoPor = await _context.Afectados.FindAsync(id);
            _context.Afectados.Remove(afectadoPor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AfectadoPorExists(int id)
        {
            return _context.Afectados.Any(e => e.AfectadoPorId == id);
        }
    }
}
