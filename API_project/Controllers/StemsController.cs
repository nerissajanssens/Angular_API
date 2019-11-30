using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StemsController : ControllerBase
    {
        private readonly PollContext _context;

        public StemsController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Stems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmen()
        {
            return await _context.Stemmen.ToListAsync();
        }

        // GET: api/Stems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stem>> GetStem(int id)
        {
            var stem = await _context.Stemmen.FindAsync(id);

            if (stem == null)
            {
                return NotFound();
            }

            return stem;
        }

        //GET: api/Stems/count/1
        //Tellen van alle stemmen per optie per poll. 
        //Deze uitkomst wordt dan toegevoegd in het count-attribuut van de tabel optie
        [HttpGet("count/{pollId}")]
        public async Task<ActionResult<Stem>> GetCount(int pollId)
        {
            var opties = await _context.Opties
                .Where(p => p.PollID == pollId)
                .ToListAsync();

            foreach (Optie optie in opties)
            {
                optie.Count =  _context.Stemmen
                .Where(s => s.OptieID == optie.OptieID)
                .Count();

                _context.Entry(optie).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        // PUT: api/Stems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStem(int id, Stem stem)
        {
            if (id != stem.StemID)
            {
                return BadRequest();
            }

            _context.Entry(stem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StemExists(id))
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

        // POST: api/Stems
        [HttpPost]
        public async Task<ActionResult<Stem>> PostStem(Stem stem)
        {
            _context.Stemmen.Add(stem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStem", new { id = stem.StemID }, stem);
        }

        // DELETE: api/Stems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stem>> DeleteStem(int id)
        {
            var stem = await _context.Stemmen.FindAsync(id);
            if (stem == null)
            {
                return NotFound();
            }

            _context.Stemmen.Remove(stem);
            await _context.SaveChangesAsync();

            return stem;
        }

        private bool StemExists(int id)
        {
            return _context.Stemmen.Any(e => e.StemID == id);
        }
    }
}
