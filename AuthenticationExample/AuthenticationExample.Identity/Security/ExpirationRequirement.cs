using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationExample.Identity.Security
{
    public class ExpirationRequirement : IAuthorizationRequirement
    {
        public int Year { get; }

        public ExpirationRequirement(int year)
        {
            Year = year;
        }
    }

    public class ExpirationRequirementHandler : AuthorizationHandler<ExpirationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExpirationRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
                return Task.CompletedTask;

            var expirationClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration);

            if (expirationClaim == null || 
                !DateTime.TryParse(expirationClaim.Value, out var expirationTime) || 
                expirationTime.Date > DateTime.UtcNow.Date)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
