using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AuthenticationExample.Security.Middlewares
{
    internal class TokenMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}
