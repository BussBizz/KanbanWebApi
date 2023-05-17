using DevOne.Security.Cryptography.BCrypt;
using KanbanWebApi.DB;
using KanbanWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace KanbanWebApi.Controllers
{
    public class AuthController : BaseController
    {
        private KanbanDBContext _context;

        public AuthController(KanbanDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> Login()
        {
            return await Authenticate(_context);
        }

        // GET: api/auth/username/password
        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<LoginDTO>> Login(string username, string password)
        {
            var user = await _context.Users.Where(u => u.Name == username).FirstOrDefaultAsync();
            if (user == null) return NotFound();

            var pwd = await _context.Passwords.Where(p => p.UserId == user.Id).FirstOrDefaultAsync();
            if (pwd == null) return NotFound();

            if (BCryptHelper.CheckPassword(password, pwd.Hash))
            {
                var token = await GenerateToken();
                return new LoginDTO { Token = token, User = user };
            }

            return BadRequest("Auth failed");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteToken()
        {
            var token = ExtractToken();
            if (token == null) return NotFound();
            
            var tokenEntity = await _context.Tokens.FindAsync(token.Id);
            if (tokenEntity == null) return NotFound();

            _context.Tokens.Remove(tokenEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<string> GenerateToken()
        {
            var token = Guid.NewGuid().ToString();
            var salt = BCryptHelper.GenerateSalt();

            var tokenEntity = new Token
            {
                Hash = BCryptHelper.HashPassword(token, salt),
                Expire = DateTime.Now.AddDays(30),
            };

            await _context.Tokens.AddAsync(tokenEntity);
            await _context.SaveChangesAsync();

            var json = JsonSerializer.Serialize(new { Id = tokenEntity.Id, Token = token });
            var bytes = Encoding.UTF8.GetBytes(json);
            var result = Convert.ToBase64String(bytes);

            return result;
        }
    }
}

