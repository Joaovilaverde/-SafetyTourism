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
    public class SurtosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurtosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Surtos
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DestinoSortParm"] = String.IsNullOrEmpty(sortOrder) ? "destinoId_desc" : "";
            ViewData["DoencaSortParm"] = sortOrder == "DoencaId" ? "doencaId_desc" : "DoencaId";
            ViewData["DataSortParm"] = sortOrder == "Data" ? "data_desc" : "Data";
            ViewData["InfectadosSortParm"] = sortOrder == "InfectadosPor100k" ? "infectados_desc" : "InfectadosPor100k";
            ViewData["GravidadeSortParm"] = sortOrder == "Gravidade" ? "gravidade_desc" : "Gravidade";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var surtos = from a in _context.Surtos.Include(a => a.Destino).Include(a => a.Doenca)
                            select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                surtos = surtos.Where(a => a.Destino.Nome.Contains(searchString)
                                       || a.Doenca.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "destinoId_desc":
                    surtos = surtos.OrderByDescending(a => a.Destino.Nome);
                    break;
                case "DoencaId":
                    surtos = surtos.OrderBy(a => a.Doenca.Nome);
                    break;
                case "doencaId_desc":
                    surtos = surtos.OrderByDescending(a => a.Doenca.Nome);
                    break;
                case "Data":
                    surtos = surtos.OrderBy(a => a.Data);
                    break;
                case "data_desc":
                    surtos = surtos.OrderByDescending(a => a.Data);
                    break;
                case "InfectadosPor100k":
                    surtos = surtos.OrderBy(a => a.InfectadosPor100k);
                    break;
                case "infectados_desc":
                    surtos = surtos.OrderByDescending(a => a.InfectadosPor100k);
                    break;
                case "Gravidade":
                    surtos = surtos.OrderBy(a => a.Gravidade);
                    break;
                case "gravidade_desc":
                    surtos = surtos.OrderByDescending(a => a.Gravidade);
                    break;
                default:
                    surtos = surtos.OrderBy(a => a.Destino.Nome);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Surto>.CreateAsync(surtos.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Surtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surto = await _context.Surtos
                .Include(s => s.Destino)
                .Include(s => s.Doenca)
                .FirstOrDefaultAsync(m => m.SurtoId == id);
            if (surto == null)
            {
                return NotFound();
            }

            return View(surto);
        }

        // GET: Surtos/Create
        public IActionResult Create()
        {
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId");
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId");
            return View();
        }

        // POST: Surtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurtoId,DestinoId,DoencaId,Data,InfectadosPor100k,Gravidade")] Surto surto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", surto.DestinoId);
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId", surto.DoencaId);
            return View(surto);
        }

        // GET: Surtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surto = await _context.Surtos.FindAsync(id);
            if (surto == null)
            {
                return NotFound();
            }
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", surto.DestinoId);
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId", surto.DoencaId);
            return View(surto);
        }

        // POST: Surtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurtoId,DestinoId,DoencaId,Data,InfectadosPor100k,Gravidade")] Surto surto)
        {
            if (id != surto.SurtoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurtoExists(surto.SurtoId))
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
            ViewData["DestinoId"] = new SelectList(_context.Destinos, "DestinoId", "DestinoId", surto.DestinoId);
            ViewData["DoencaId"] = new SelectList(_context.Doencas, "DoencaId", "DoencaId", surto.DoencaId);
            return View(surto);
        }

        // GET: Surtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surto = await _context.Surtos
                .Include(s => s.Destino)
                .Include(s => s.Doenca)
                .FirstOrDefaultAsync(m => m.SurtoId == id);
            if (surto == null)
            {
                return NotFound();
            }

            return View(surto);
        }

        // POST: Surtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surto = await _context.Surtos.FindAsync(id);
            _context.Surtos.Remove(surto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurtoExists(int id)
        {
            return _context.Surtos.Any(e => e.SurtoId == id);
        }
        private void PopulateDestinosDropDownList(object selectedDestino = null)
        {
            var destinosQuery = from d in _context.Destinos
                                orderby d.Nome
                                select d;
            ViewBag.DestinoId = new SelectList(destinosQuery.AsNoTracking(), "DestinoId", "Nome", selectedDestino);
        }
        private void PopulateDoencasDropDownList(object selectedDoenca = null)
        {
            var doencasQuery = from r in _context.Doencas
                               orderby r.Nome
                               select r;
            ViewBag.DoencaId = new SelectList(doencasQuery.AsNoTracking(), "DoencaId", "Nome", selectedDoenca);
        }
    }
}
