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
    public class OptiesController : ControllerBase
    {
        private readonly PollContext _context;

        public OptiesController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Opties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Optie>>> GetOpties()
        {
            return await _context.Opties
                .Include(p => p.Poll)
                .Include(p => p.Stemmen)
                .ToListAsync();
        }

        // GET: api/Opties/2
        [HttpGet("{pollId}")]
        public async Task<ActionResult<IEnumerable<Optie>>> GetOptiesByPoll(int pollId)
        {
            return await _context.Opties
                .Where(p => p.PollID == pollId)
                .ToListAsync();
        }

        //// GET: api/Opties/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Optie>> GetOptie(int id)
        //{
        //    var optie = await _context.Opties.FindAsync(id);

        //    if (optie == null)
        //    {
        //        return NotFound();
        //    }

        //    return optie;
        //}

        // PUT: api/Opties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOptie(int id, Optie optie)
        {
            if (id != optie.OptieID)
            {
                return BadRequest();
            }

            _context.Entry(optie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptieExists(id))
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

        // POST: api/Opties
        [HttpPost]
        public async Task<ActionResult<Optie>> PostOptie(Optie optie)
        {
            _context.Opties.Add(optie);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetOptie", new { id = optie.OptieID }, optie);
        }

        // DELETE: api/Opties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Optie>> DeleteOptie(int id)
        {
            var optie = await _context.Opties.FindAsync(id);
            if (optie == null)
            {
                return NotFound();
            }

            _context.Opties.Remove(optie);
            await _context.SaveChangesAsync();

            return optie;
        }

        private bool OptieExists(int id)
        {
            return _context.Opties.Any(e => e.OptieID == id);
        }
    }
}
