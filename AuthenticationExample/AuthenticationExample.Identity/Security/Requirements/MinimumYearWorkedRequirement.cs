using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationExample.Identity.Security.Requirements
{
    public class MinimumYearWorkedRequirement : IAuthorizationRequirement
    {
        public int Year { get; }

        public MinimumYearWorkedRequirement(int year)
        {
            Year = year;
        }
    }

    public class MinimumYearWorkedRequirementHandler : AuthorizationHandler<MinimumYearWorkedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumYearWorkedRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
                return Task.CompletedTask;

            var hireDate = context.User.Claims.FirstOrDefault(x => x.Type == SystemClaimTypes.HireDate);

            if (hireDate == null || 
                !DateTime.TryParse(hireDate.Value, out var date) || 
                date.Date.Year < requirement.Year)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
