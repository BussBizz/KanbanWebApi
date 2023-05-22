using KanbanWebApi.DB;
using KanbanWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanWebApi.Controllers
{
    public class MembersController : BaseController
    {
        private readonly KanbanDBContext _context;

        public MembersController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Members == null)
            {
                return NotFound();
            }
            return await _context.Members.ToListAsync();
        }

        // GET: api/Members/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembershipsByUser(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Members == null)
            {
                return NotFound();
            }

            var memberships = await _context.Members
                .Where(m => m.UserId == id)
                .Include(m => m.Board)
                .Include(m => m.TasksAssigned)
                .ToListAsync();

            return memberships;
        }

        // GET: api/Members/board/5
        [HttpGet("board/{boardId}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembershipsByBoard(int boardId)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Members == null)
            {
                return NotFound();
            }

            var memberships = await _context.Members
                .Where(m => m.BoardId == boardId)
                .Include(m => m.User)
                .ToListAsync();

            return memberships;
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (id != member.Id)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Members == null)
            {
                return Problem("Entity set 'KanbanDBContext.Members'  is null.");
            }
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members
                .Include(m => m.TasksCreated)
                .Include(m => m.TasksCompleted)
                .Include(m => m.TasksAssigned)
                .Include(m => m.Categories)
                .Include(m => m.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
