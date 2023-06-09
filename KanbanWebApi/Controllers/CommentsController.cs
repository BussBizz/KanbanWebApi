﻿using KanbanWebApi.DB;
using KanbanWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanWebApi.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly KanbanDBContext _context;

        public CommentsController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // GET: api/Comments/task/5
        [HttpGet("task/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByTask(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments
                .Where(c => c.KanbanTaskId == id)
                .Include(c => c.Member)
                .ThenInclude(m => m.User)
                .ToListAsync();

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Comments == null)
            {
                return Problem("Entity set 'KanbanDBContext.Comments'  is null.");
            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
