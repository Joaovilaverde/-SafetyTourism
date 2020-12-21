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
    [Route("api/Virus")]
    [ApiController]
    public class VirusController : ControllerBase
    {
        private readonly OMSContext _context;

        public VirusController(OMSContext context)
        {
            _context = context;
        }

        // GET: api/Virus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Virus>>> GetVirus()
        {
            return await _context.Virus.ToListAsync();
        }

        // GET: api/Virus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Virus>> GetVirus(long id)
        {
            var virus = await _context.Virus.FindAsync(id);

            if (virus == null)
            {
                return NotFound();
            }

            return virus;
        }

        // PUT: api/Virus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVirus(long id, Virus virus)
        {
            if (id != virus.Id)
            {
                return BadRequest();
            }

            _context.Entry(virus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VirusExists(id))
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

        // POST: api/Virus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Virus>> PostVirus(Virus virus)
        {
            _context.Virus.Add(virus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVirus", new { id = virus.Id }, virus);
        }

        // DELETE: api/Virus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVirus(long id)
        {
            var virus = await _context.Virus.FindAsync(id);
            if (virus == null)
            {
                return NotFound();
            }

            _context.Virus.Remove(virus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VirusExists(long id)
        {
            return _context.Virus.Any(e => e.Id == id);
        }
    }
}
