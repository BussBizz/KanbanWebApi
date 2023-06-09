﻿using DevOne.Security.Cryptography.BCrypt;
using KanbanWebApi.DB;
using KanbanWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace KanbanWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public async Task<bool> Authenticate(KanbanDBContext context)
        {
            var bearerToken = ExtractToken();
            if (bearerToken == null) return false;

            var tokenEntity = await context.Tokens.Where(t => t.Id == bearerToken.Id).FirstOrDefaultAsync(t => t.Expire > DateTime.Now);
            if (tokenEntity == null) return false;

            if (BCryptHelper.CheckPassword(bearerToken.Token, tokenEntity.Hash))
            {
                tokenEntity.Expire = DateTime.Now.AddDays(30);
                context.Update(tokenEntity);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        [NonAction]
        public BearerToken? ExtractToken()
        {
            string? authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith("Bearer"))
                return null;

            string encodedUsernamePassword = authHeader.Substring("Bearer ".Length).Trim();

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            var tokenString = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            return JsonSerializer.Deserialize<BearerToken>(tokenString);
        }
    }
}
