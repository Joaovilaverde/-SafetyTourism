using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
            return await _context.Surtos.ToListAsync();
        }

        // GET: api/Surtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Surto>> GetSurto(long id)
        {
            var surto = await _context.Surtos.FindAsync(id);

            if (surto == null)
            {
                return NotFound();
            }

            return surto;
        }

        // GET: Obter os surtos ativos para o país referido
        [Route("~/api/paises/{paisId}/surtos")]
        public async Task<IQueryable<Surto>> GetSurtoByPaisAsync(string paisId)
        {
            Pais pais = await _context.Paises.FindAsync(paisId);
            return _context.Surtos.Include(s => s.VirusId).Include(s => s.ZonaId).Where(s => s.ZonaId == pais.ZonaId);
        }

        //GET: Obter informação sobre todos os surtos ativos associados ao vírus referido
        [Route("~/api/surtos/virus/{Id}")]
        public IQueryable<Surto> GetVirusById(long Id)
        {
            return _context.Surtos.Include(s => s.Virus).Include(s => s.Zona).Where(s => s.VirusId == Id && s.DataFim == null);
        }

        //GET: Obter informação todos os surtos ocorridos associados ao vírus referido
        [Route("~/api/virus/{Id}/surtos")]
        public IQueryable<Surto> GetSurtosById(long Id)
        {
            return _context.Surtos.Include(b => b.Virus).Include(b => b.Zona).Where(b => b.VirusId == Id);
        }

        // PUT: api/Surtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurto(long id, Surto surto)
        {
            if (id != surto.Id)
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
        /*
        // PATCH: Alterar a data de fim do surto
        [Route("~/api/surtos/{zonaId}/virusId")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Surto> patchEntity)
        {
            var entity = _context.Surtos.Find(id);

            if (entity == null)
            {
                return NotFound();
            }

            patchEntity.ApplyTo(entity, ModelState); // Must have Microsoft.AspNetCore.Mvc.NewtonsoftJson installed

            return Ok(entity);
        }*/

        // POST: api/Surtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Surto>> PostSurto(Surto surto)
        {
            _context.Surtos.Add(surto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurto", new { id = surto.Id }, surto);
        }

        // DELETE: api/Surtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurto(long id)
        {
            var surto = await _context.Surtos.FindAsync(id);
            if (surto == null)
            {
                return NotFound();
            }

            _context.Surtos.Remove(surto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurtoExists(long id)
        {
            return _context.Surtos.Any(e => e.Id == id);
        }
    }
}
