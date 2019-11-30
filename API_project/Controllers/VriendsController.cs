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
    public class VriendsController : ControllerBase
    {
        private readonly PollContext _context;

        public VriendsController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Vriends/5
        [HttpGet("{gebruikersid}")]
        public async Task<ActionResult<IEnumerable<Vriend>>>GetVriend(int gebruikersid)
        {
            var vrienden = await _context.Vrienden
                .Include(g => g.GebruikerFrom)
                .Include(g => g.GebruikerTo)
                .Where(g => g.friendFrom == gebruikersid && g.bevestigd == true)
                .ToListAsync();

            if (vrienden == null)
            {
                return NotFound();
            }

            return vrienden;
        }

        //GET: api/Vriends/Verzoeken/5
        [HttpGet("verzoeken/{gebruikersid}")]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVriendVerzoeken(int gebruikersid)
        {
            var vrienden = await _context.Vrienden
                .Include(g => g.GebruikerFrom)
                .Include(g => g.GebruikerTo)
                .Where(g =>g.friendFrom != gebruikersid && g.bevestigd == false)
                .ToListAsync();

            if (vrienden == null)
            {
                return NotFound();
            }

            return vrienden;
        }

        // PUT: api/Vriends/confirm/5
        [HttpPut("confirm/{id}")]
        public async Task<IActionResult> ConfirmFriend(int id)
        {

            Vriend vriend = await _context.Vrienden.FindAsync(id);

            if (vriend.bevestigd == false)
            {
                vriend.bevestigd = true;
            }

            _context.Entry(vriend).State = EntityState.Modified;
           try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VriendExists(id))
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

        // PUT: api/Vriends/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVriend(int id, Vriend vriend)
        {
            if (id != vriend.vriendID)
            {
                return BadRequest();
            }

            _context.Entry(vriend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VriendExists(id))
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

        // POST: api/Vriends
        [HttpPost("VriendToevoegen")]
        public async Task<ActionResult<Vriend>> PostVriend(Vriend vriend)
        {
            _context.Vrienden.Add(vriend);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetVriend", new { id = vriend.vriendID }, vriend);
        }

        // DELETE: api/Vriends/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vriend>> DeleteVriend(int id)
        {
            var vriend = await _context.Vrienden.FindAsync(id);
            if (vriend == null)
            {
                return NotFound();
            }

            _context.Vrienden.Remove(vriend);
            await _context.SaveChangesAsync();

            return vriend;
        }

        private bool VriendExists(int id)
        {
            return _context.Vrienden.Any(e => e.vriendID == id);
        }
    }
}
