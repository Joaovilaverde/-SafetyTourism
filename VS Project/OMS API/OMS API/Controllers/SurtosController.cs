using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS_API.Data;
using OMS_API.Models;

namespace OMS_API.Controllers
{
    [Route("api/surtos")]
    [ApiController]
    public class SurtosController : ControllerBase
    {
        private readonly OMSContext _context;

        public SurtosController(OMSContext context)
        {
            _context = context;
        }

        // GET: api/Surtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Surto>>> GetSurto()
        {
            return await _context.Surto.ToListAsync();
        }

        // GET: api/Surtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Surto>> GetSurto(long id)
        {
            var surto = await _context.Surto.FindAsync(id);

            if (surto == null)
            {
                return NotFound();
            }

            return surto;
        }

            [Route("~/api/paises/{paisId}/surtos")]
        public async Task<IQueryable<Surto>> GetSurtoByPaisAsync(string paisId)
        {
            Pais pais = await _context.Pais.FindAsync(paisId);
            return _context.Surto.Include(s => s.VirusId).Include(s => s.ZonaId).Where(s => s.ZonaId == pais.ZonaId);
        }


        // PUT: api/Surtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurto(long id, Surto surto)
        {
            if (id != surto.SurtoId)
            {
                return BadRequest();
            }

            _context.Entry(surto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurtoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Surtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Surto>> PostSurto(Surto surto)
        {
            _context.Surto.Add(surto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurto", new { id = surto.SurtoId }, surto);
        }

        // DELETE: api/Surtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurto(long id)
        {
            var surto = await _context.Surto.FindAsync(id);
            if (surto == null)
            {
                return NotFound();
            }

            _context.Surto.Remove(surto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurtoExists(long id)
        {
            return _context.Surto.Any(e => e.SurtoId == id);
        }
    }
}
