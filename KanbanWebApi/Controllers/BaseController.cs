using DevOne.Security.Cryptography.BCrypt;
using KanbanWebApi.DB;
using KanbanWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

            var passwordId = usernamePassword.Substring(0, seperatorIndex);
            var password = usernamePassword.Substring(seperatorIndex + 1);

            Password pwd = await context.Passwords.FindAsync(passwordId) ?? throw new Exception("Error checking password.");

            var result = BCryptHelper.CheckPassword(password, pwd.Hash);

            return result;
        }
    }
}
