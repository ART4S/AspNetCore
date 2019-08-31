using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Web.Extensions
{
    public static class WebHostExtensions
    {
        public static void UpdateDb(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<DbContext>();
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    var logger = scope.ServiceProvider.GetService<ILoggerFactory>().CreateLogger("UpdateDb");
                    logger.LogWarning(e.Message);
                }
            }
        }
    }
}
