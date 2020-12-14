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
    public class DoencasController : Controller
    {
        private readonly SafetyContext _context;

        public DoencasController(SafetyContext context)
        {
            _context = context;
        }

        // GET: Doencas
        public async Task<IActionResult> Index()
        {
            var safetyContext = _context.Doencas.Include(d => d.Recomendacao);
            return View(await safetyContext.ToListAsync());
        }

        // GET: Doencas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doenca = await _context.Doencas
                .Include(d => d.Recomendacao)
                .FirstOrDefaultAsync(m => m.DoencaId == id);
            if (doenca == null)
            {
                return NotFound();
            }

            return View(doenca);
        }

        // GET: Doencas/Create
        public IActionResult Create()
        {
            //ViewData["RecomendacaoId"] = new SelectList(_context.Recomendacoes, "RecomendacaoId", "RecomendacaoId");
            PopulateRecomendacaoDropDownList();
            return View();
        }

        // POST: Doencas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoencaId,Nome,Descricao,RecomendacaoId")] Doenca doenca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doenca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["RecomendacaoId"] = new SelectList(_context.Recomendacoes, "RecomendacaoId", "RecomendacaoId", doenca.RecomendacaoId);
            PopulateRecomendacaoDropDownList(doenca.RecomendacaoId);
            return View(doenca);
        }

        // GET: Doencas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doenca = await _context.Doencas.FindAsync(id);
            if (doenca == null)
            {
                return NotFound();
            }
            //ViewData["RecomendacaoId"] = new SelectList(_context.Recomendacoes, "RecomendacaoId", "RecomendacaoId", doenca.RecomendacaoId);
            PopulateRecomendacaoDropDownList(doenca.RecomendacaoId);
            return View(doenca);
        }

        // POST: Doencas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoencaId,Nome,Descricao,RecomendacaoId")] Doenca doenca)
        {
            if (id != doenca.DoencaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doenca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoencaExists(doenca.DoencaId))
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
            //ViewData["RecomendacaoId"] = new SelectList(_context.Recomendacoes, "RecomendacaoId", "RecomendacaoId", doenca.RecomendacaoId);
            PopulateRecomendacaoDropDownList(doenca.RecomendacaoId);
            return View(doenca);
        }

        // GET: Doencas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doenca = await _context.Doencas
                .Include(d => d.Recomendacao)
                .FirstOrDefaultAsync(m => m.DoencaId == id);
            if (doenca == null)
            {
                return NotFound();
            }

            return View(doenca);
        }

        // POST: Doencas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doenca = await _context.Doencas.FindAsync(id);
            _context.Doencas.Remove(doenca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoencaExists(int id)
        {
            return _context.Doencas.Any(e => e.DoencaId == id);
        }
        private void PopulateRecomendacaoDropDownList(object selectedRecomendacao = null)
        {
            var recomendacaoQuery = from r in _context.Recomendacoes
                                orderby r.Nome
                                select r;
            ViewBag.RecomendacaoId = new SelectList(recomendacaoQuery.AsNoTracking(), "RecomendacaoId", "Nome", selectedRecomendacao);
        }
    }
}
