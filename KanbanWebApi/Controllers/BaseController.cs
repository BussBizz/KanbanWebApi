using DevOne.Security.Cryptography.BCrypt;
using KanbanWebApi.DB;
using KanbanWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace KanbanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public async Task<bool> Authenticate(KanbanDBContext context)
        {
            string? authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith("Basic"))
                throw new Exception("The authorization header is either empty or isn't Basic.");

            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            int seperatorIndex = usernamePassword.IndexOf(':');

            var username = usernamePassword.Substring(0, seperatorIndex);
            var password = usernamePassword.Substring(seperatorIndex + 1);

            var user = await context.Users.Where(u => u.Name == username).FirstOrDefaultAsync();

            if (user == null) return false;

            var pwd = await context.Passwords.Where(p => p.UserId == user.Id).FirstOrDefaultAsync();
            
            if (pwd == null) return false;

            if (BCryptHelper.CheckPassword(password, pwd.Hash))
            {
                return true;
            }

            return false;
        }
    }
}
