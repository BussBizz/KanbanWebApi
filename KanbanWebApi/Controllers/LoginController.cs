using DevOne.Security.Cryptography.BCrypt;
using KanbanWebApi.DB;
using KanbanWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace KanbanWebApi.Controllers
{
    public class LoginController : BaseController
    {
        private KanbanDBContext _context;

        public LoginController(KanbanDBContext context)
        {
            _context = context;
        }

        [HttpGet("{username}/check")]
        public async Task<ActionResult<bool>> CheckUsername(string username)
        {
            if (username == null || _context.Users == null) return NotFound();

            var nameExists = await _context.Users.AnyAsync(u => u.Name == username);

            return nameExists;
        }

        // TODO Use Auth header mayby, hotel trivago?
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> Login(string username)
        {
            if(!string.IsNullOrEmpty(username) && await Authenticate(_context))
            {
                return await _context.Users.Where(u => u.Name == username).FirstOrDefaultAsync();
            }

            return BadRequest();
        }
    }
}

