﻿using Hangfire.Dashboard;

namespace HangfireExample.Server.Filters
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}
