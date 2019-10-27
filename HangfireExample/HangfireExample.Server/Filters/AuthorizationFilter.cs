using Hangfire.Dashboard;

namespace HangfireExample.Server.Filters
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return context.GetHttpContext().User.Identity.IsAuthenticated;
        }
    }
}
