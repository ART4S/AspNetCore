using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationExample.Security.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("access_token", out var values) && !values.Any())
            {
                await NotAuthorizedResponse(context);
                return;
            }

            var token = values[0];
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                await NotAuthorizedResponse(context);
                return;
            }

            context.User = new ClaimsPrincipal(
                new ClaimsIdentity(tokenHandler.ReadJwtToken(token).Claims));

            await _next(context);
        }

        private async Task NotAuthorizedResponse(HttpContext context)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Token is invalid");
        }
    }
}
