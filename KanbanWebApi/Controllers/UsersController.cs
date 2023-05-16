using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanWebApi.DB;
using KanbanWebApi.Models;
using DevOne.Security.Cryptography.BCrypt;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KanbanWebApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly KanbanDBContext _context;

        public UsersController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/username
        [HttpGet("name/{username}")]
        public async Task<ActionResult<User>> GetUserIdFromName(string username)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.Where(u => u.Name == username).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(Password password)
        {
            if (_context.Users == null || _context.Passwords == null)
            {
                return Problem("Entity set 'KanbanDBContext.Users'  is null.");
            }

            var salt = BCryptHelper.GenerateSalt();
            password.Hash = BCryptHelper.HashPassword(password.Hash, salt);

            _context.Users.Add(password.User);
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = password.User.Id }, password.User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return BadRequest();
#endif
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
