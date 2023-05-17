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
    public class KanbanTasksController : BaseController
    {
        private readonly KanbanDBContext _context;

        public KanbanTasksController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/KanbanTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanTask>>> GetKanbanTasks()
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.KanbanTasks == null)
            {
                return NotFound();
            }
            return await _context.KanbanTasks.ToListAsync();
        }

        // GET: api/KanbanTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KanbanTask>> GetKanbanTask(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.KanbanTasks == null)
            {
                return NotFound();
            }
            var kanbanTask = await _context.KanbanTasks.FindAsync(id);

            if (kanbanTask == null)
            {
                return NotFound();
            }

            return kanbanTask;
        }

        // PUT: api/KanbanTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKanbanTask(int id, KanbanTask kanbanTask)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (id != kanbanTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(kanbanTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KanbanTaskExists(id))
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

        // POST: api/KanbanTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KanbanTask>> PostKanbanTask(KanbanTask kanbanTask)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.KanbanTasks == null)
            {
                return Problem("Entity set 'KanbanDBContext.KanbanTasks'  is null.");
            }
            _context.KanbanTasks.Add(kanbanTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKanbanTask", new { id = kanbanTask.Id }, kanbanTask);
        }

        // DELETE: api/KanbanTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKanbanTask(int id)
        {
#if RELEASE
            if (!await Authenticate(_context)) return Unauthorized();
#endif
            if (_context.KanbanTasks == null)
            {
                return NotFound();
            }
            var kanbanTask = await _context.KanbanTasks.FindAsync(id);
            if (kanbanTask == null)
            {
                return NotFound();
            }

            _context.KanbanTasks.Remove(kanbanTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KanbanTaskExists(int id)
        {
            return (_context.KanbanTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
