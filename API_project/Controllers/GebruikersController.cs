using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_project.Models;
using API_project.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikersController : Controller
    {
        private readonly PollContext _context;
        private IUserService _userService;

        public GebruikersController(PollContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Gebruiker userParam)
        {
            var user = _userService.Authenticate(userParam.Gebruikersnaam, userParam.Wachtwoord);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password incorrect" });
            }
            return Ok(user);
        }

        // GET: api/Gebruikers
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Gebruiker>>> GetGebruikers()
        {
            return await _context.Gebruikers
               .Include(g => g.PollGebruikers)
                .Include(g => g.Stemmen)
                .Include(g => g.Verzonden)
                .Include(g => g.Gekregen)
                .ToListAsync();
        }

        // GET: api/Gebruikers/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Gebruiker>> GetGebruiker(int id)
        {
            var gebruiker = await _context.Gebruikers
                .Include(g => g.PollGebruikers)
                .ThenInclude(pollGebruiker => pollGebruiker.Poll)
                .Include(g => g.Stemmen)
                .Include(g => g.Verzonden)
                .Include(g => g.Gekregen)
                .FirstOrDefaultAsync(g => g.GebruikerID == id);

            if (gebruiker == null)
            {
                return NotFound();
            }

            return gebruiker;
        }

        // GET: api/Gebruikers/naam/jef
        [HttpGet("naam/{naam}")]
        [Authorize]
        public async Task<ActionResult<Gebruiker>> GetGebruiker(string naam)
        {
            var gebruiker = await _context.Gebruikers
                .Include(g => g.PollGebruikers)
                .ThenInclude(pollGebruiker => pollGebruiker.Poll)
                .Include(g => g.Stemmen)
                .Include(g => g.Verzonden)
                .Include(g => g.Gekregen)
                .FirstOrDefaultAsync(g => g.Gebruikersnaam == naam);

            if (gebruiker == null)
            {
                return NotFound();
            }

            return gebruiker;
        }

        // PUT: api/Gebruikers/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutGebruiker(int id, Gebruiker gebruiker)
        {
            if (id != gebruiker.GebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(gebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GebruikerExists(id))
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

        // POST: api/Gebruikers
        [HttpPost]
        public async Task<ActionResult<Gebruiker>> PostGebruiker(Gebruiker gebruiker)
        {
            _context.Gebruikers.Add(gebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.GebruikerID }, gebruiker);
        }

        // DELETE: api/Gebruikers/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Gebruiker>> DeleteGebruiker(int id)
        {
            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            _context.Gebruikers.Remove(gebruiker);
            await _context.SaveChangesAsync();

            return gebruiker;
        }

        private bool GebruikerExists(int id)
        {
            return _context.Gebruikers.Any(e => e.GebruikerID == id);
        }
    }
}
