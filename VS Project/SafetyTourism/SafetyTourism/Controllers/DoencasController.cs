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

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NomeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var doencas = from d in _context.Doencas
            select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                doencas = doencas.Where(d => d.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nome_desc":
                    doencas = doencas.OrderByDescending(d => d.Nome);
                    break;
                default:
                    doencas = doencas.OrderBy(d => d.Nome);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Doenca>.CreateAsync(doencas.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Doencas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doenca = await _context.Doencas
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
            return View();
        }

        // POST: Doencas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoencaId,Nome,Descricao,Recomendacao")] Doenca doenca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doenca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["RecomendacaoId"] = new SelectList(_context.Recomendacoes, "RecomendacaoId", "RecomendacaoId", doenca.RecomendacaoId);
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
            return View(doenca);
        }

        // POST: Doencas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoencaId,Nome,Descricao,Recomendacao")] Doenca doenca)
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
    }
}
