using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanWebApi.DB;
using KanbanWebApi.Models;

namespace KanbanWebApi.Controllers
{
    public class InvitesController : BaseController
    {
        private readonly KanbanDBContext _context;

        public InvitesController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/Invites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invite>>> GetInvites()
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Invites == null)
            {
                return NotFound();
            }
            return await _context.Invites.ToListAsync();
        }

        // GET: api/Invites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invite>> GetInvite(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Invites == null)
            {
                return NotFound();
            }
            var invite = await _context.Invites.FindAsync(id);

            if (invite == null)
            {
                return NotFound();
            }

            return invite;
        }

        // PUT: api/Invites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvite(int id, Invite invite)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (id != invite.Id)
            {
                return BadRequest();
            }

            _context.Entry(invite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InviteExists(id))
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

        // POST: api/Invites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invite>> PostInvite(Invite invite)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Invites == null)
            {
                return Problem("Entity set 'KanbanDBContext.Invites'  is null.");
            }
            _context.Invites.Add(invite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvite", new { id = invite.Id }, invite);
        }

        // DELETE: api/Invites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvite(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Invites == null)
            {
                return NotFound();
            }
            var invite = await _context.Invites.FindAsync(id);
            if (invite == null)
            {
                return NotFound();
            }

            _context.Invites.Remove(invite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InviteExists(int id)
        {
            return (_context.Invites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
