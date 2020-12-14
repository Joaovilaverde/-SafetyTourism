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
    public class RecomendacoesController : Controller
    {
        private readonly SafetyContext _context;

        public RecomendacoesController(SafetyContext context)
        {
            _context = context;
        }

        // GET: Recomendacoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recomendacoes.ToListAsync());
        }

        // GET: Recomendacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomendacao = await _context.Recomendacoes
                .FirstOrDefaultAsync(m => m.RecomendacaoId == id);
            if (recomendacao == null)
            {
                return NotFound();
            }

            return View(recomendacao);
        }

        // GET: Recomendacoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recomendacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecomendacaoId,Nome,Conteudo")] Recomendacao recomendacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recomendacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recomendacao);
        }

        // GET: Recomendacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomendacao = await _context.Recomendacoes.FindAsync(id);
            if (recomendacao == null)
            {
                return NotFound();
            }
            return View(recomendacao);
        }

        // POST: Recomendacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecomendacaoId,Nome,Conteudo")] Recomendacao recomendacao)
        {
            if (id != recomendacao.RecomendacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recomendacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecomendacaoExists(recomendacao.RecomendacaoId))
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
            return View(recomendacao);
        }

        // GET: Recomendacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomendacao = await _context.Recomendacoes
                .FirstOrDefaultAsync(m => m.RecomendacaoId == id);
            if (recomendacao == null)
            {
                return NotFound();
            }

            return View(recomendacao);
        }

        // POST: Recomendacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recomendacao = await _context.Recomendacoes.FindAsync(id);
            _context.Recomendacoes.Remove(recomendacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecomendacaoExists(int id)
        {
            return _context.Recomendacoes.Any(e => e.RecomendacaoId == id);
        }
    }
}
