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
    public class KanbanCommentsController : BaseController
    {
        private readonly KanbanDBContext _context;

        public KanbanCommentsController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/KanbanComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanComment>>> GetKanbanComments()
        {
          if (_context.KanbanComments == null)
          {
              return NotFound();
          }
            return await _context.KanbanComments.ToListAsync();
        }

        // GET: api/KanbanComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KanbanComment>> GetKanbanComment(int id)
        {
          if (_context.KanbanComments == null)
          {
              return NotFound();
          }
            var kanbanComment = await _context.KanbanComments.FindAsync(id);

            if (kanbanComment == null)
            {
                return NotFound();
            }

            return kanbanComment;
        }

        // PUT: api/KanbanComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKanbanComment(int id, KanbanComment kanbanComment)
        {
            if (id != kanbanComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(kanbanComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KanbanCommentExists(id))
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

        // POST: api/KanbanComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KanbanComment>> PostKanbanComment(KanbanComment kanbanComment)
        {
          if (_context.KanbanComments == null)
          {
              return Problem("Entity set 'KanbanDBContext.KanbanComments'  is null.");
          }
            _context.KanbanComments.Add(kanbanComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKanbanComment", new { id = kanbanComment.Id }, kanbanComment);
        }

        // DELETE: api/KanbanComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKanbanComment(int id)
        {
            if (_context.KanbanComments == null)
            {
                return NotFound();
            }
            var kanbanComment = await _context.KanbanComments.FindAsync(id);
            if (kanbanComment == null)
            {
                return NotFound();
            }

            _context.KanbanComments.Remove(kanbanComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KanbanCommentExists(int id)
        {
            return (_context.KanbanComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
