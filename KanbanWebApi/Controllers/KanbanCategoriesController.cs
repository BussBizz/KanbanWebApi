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
    public class KanbanCategoriesController : BaseController
    {
        private readonly KanbanDBContext _context;

        public KanbanCategoriesController(KanbanDBContext context)
        {
            _context = context;
        }

        // GET: api/KanbanCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanbanCategory>>> GetKanbanCategories()
        {
          if (_context.KanbanCategories == null)
          {
              return NotFound();
          }
            return await _context.KanbanCategories.ToListAsync();
        }

        // GET: api/KanbanCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KanbanCategory>> GetKanbanCategory(int id)
        {
          if (_context.KanbanCategories == null)
          {
              return NotFound();
          }
            var kanbanCategory = await _context.KanbanCategories.FindAsync(id);

            if (kanbanCategory == null)
            {
                return NotFound();
            }

            return kanbanCategory;
        }

        // PUT: api/KanbanCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKanbanCategory(int id, KanbanCategory kanbanCategory)
        {
            if (id != kanbanCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(kanbanCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KanbanCategoryExists(id))
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

        // POST: api/KanbanCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KanbanCategory>> PostKanbanCategory(KanbanCategory kanbanCategory)
        {
          if (_context.KanbanCategories == null)
          {
              return Problem("Entity set 'KanbanDBContext.KanbanCategories'  is null.");
          }
            _context.KanbanCategories.Add(kanbanCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKanbanCategory", new { id = kanbanCategory.Id }, kanbanCategory);
        }

        // DELETE: api/KanbanCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKanbanCategory(int id)
        {
            if (_context.KanbanCategories == null)
            {
                return NotFound();
            }
            var kanbanCategory = await _context.KanbanCategories.FindAsync(id);
            if (kanbanCategory == null)
            {
                return NotFound();
            }

            _context.KanbanCategories.Remove(kanbanCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KanbanCategoryExists(int id)
        {
            return (_context.KanbanCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
