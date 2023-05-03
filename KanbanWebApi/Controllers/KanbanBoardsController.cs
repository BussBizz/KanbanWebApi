using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanbanWebApi.DB;
using KanbanWebApi.Models;

namespace KanbanWebApi.Controllers
{
    public class KanbanBoardsController : BaseController
    {
        private readonly KanbanDBContext _context;

        public KanbanBoardsController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/KanbanBoards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanBoard>>> GetKanbanBoards()
        {
            if (_context.KanbanBoards == null)
            {
                return NotFound();
            }
            return await _context.KanbanBoards.ToListAsync();
        }

        // GET: api/KanbanBoards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KanbanBoard>> GetKanbanBoard(int id)
        {
            if (_context.KanbanBoards == null)
            {
                return NotFound();
            }
            var kanbanBoard = await _context.KanbanBoards.FindAsync(id);

            if (kanbanBoard == null)
            {
                return NotFound();
            }

            return kanbanBoard;
        }

        // PUT: api/KanbanBoards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKanbanBoard(int id, KanbanBoard kanbanBoard)
        {
            if (id != kanbanBoard.Id)
            {
                return BadRequest();
            }

            _context.Entry(kanbanBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KanbanBoardExists(id))
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

        // POST: api/KanbanBoards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KanbanBoard>> PostKanbanBoard(KanbanBoard kanbanBoard)
        {
            if (_context.KanbanBoards == null)
            {
                return Problem("Entity set 'KanbanDBContext.KanbanBoards'  is null.");
            }
            _context.KanbanBoards.Add(kanbanBoard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKanbanBoard", new { id = kanbanBoard.Id }, kanbanBoard);
        }

        // DELETE: api/KanbanBoards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKanbanBoard(int id)
        {
            if (_context.KanbanBoards == null)
            {
                return NotFound();
            }
            var kanbanBoard = await _context.KanbanBoards.FindAsync(id);
            if (kanbanBoard == null)
            {
                return NotFound();
            }

            _context.KanbanBoards.Remove(kanbanBoard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KanbanBoardExists(int id)
        {
            return (_context.KanbanBoards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
