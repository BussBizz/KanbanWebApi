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

        [HttpGet("{username}")]
        public async Task<ActionResult<bool>> CheckUsername(string username)
        {
            if (username == null || _context.Users == null) return NotFound();

            var nameExists = await _context.Users.AnyAsync(u => u.Name == username);

            return nameExists;
        }

        // TODO Use Auth header mayby, hotel trivago?
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return NotFound(); 

            var user = await _context.Users.Where(u => u.Name == username).FirstOrDefaultAsync();

            if (user == null) return NotFound();

            var pwd = await _context.Passwords.Where(p => p.UserId == user.Id).FirstOrDefaultAsync();

            if (pwd == null) return NotFound();

            if (BCryptHelper.CheckPassword(password, pwd.Hash))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}

