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
    public class PollGebruikersController : ControllerBase
    {
        private readonly PollContext _context;

        public PollGebruikersController(PollContext context)
        {
            _context = context;
        }

        // GET: api/PollGebruikers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruikers()
        {
            return await _context.PollGebruikers
                .Include(g => g.Gebruiker)
                .Include(p => p.Poll)
                .ToListAsync();
        }

        // GET: api/PollGebruikers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollGebruiker>> GetPollGebruiker(int id)
        {
            var pollGebruiker = await _context.PollGebruikers
                .Include(g => g.Gebruiker)
                .Include(p => p.Poll)
                .FirstOrDefaultAsync(g => g.PollGebruikerID == id);

            if (pollGebruiker == null)
            {
                return NotFound();
            }

            return pollGebruiker;
        }

        // PUT: api/PollGebruikers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollGebruiker(int id, PollGebruiker pollGebruiker)
        {
            if (id != pollGebruiker.PollGebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(pollGebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollGebruikerExists(id))
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

        // POST: api/PollGebruikers
        [HttpPost]
        public async Task<ActionResult<PollGebruiker>> PostPollGebruiker(PollGebruiker pollGebruiker)
        {
            _context.PollGebruikers.Add(pollGebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollGebruiker", new { id = pollGebruiker.PollGebruikerID }, pollGebruiker);
        }

        // DELETE: api/PollGebruikers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollGebruiker>> DeletePollGebruiker(int id)
        {
            var pollGebruiker = await _context.PollGebruikers.FindAsync(id);
            if (pollGebruiker == null)
            {
                return NotFound();
            }

            _context.PollGebruikers.Remove(pollGebruiker);
            await _context.SaveChangesAsync();

            return pollGebruiker;
        }

        private bool PollGebruikerExists(int id)
        {
            return _context.PollGebruikers.Any(e => e.PollGebruikerID == id);
        }
    }
}
