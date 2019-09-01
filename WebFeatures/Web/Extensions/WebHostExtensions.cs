using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace Web.Extensions
{
    /// <summary>
    /// Расширения для работы с хостом приложения
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Обновить базу данных приложения
        /// </summary>
        public static void UpdateDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<DbContext>();
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.Migrate();
                    }
                }
                catch (Exception e)
                {
                    var logger = scope.ServiceProvider.GetService<ILoggerFactory>().CreateLogger("UpdateDatabase");
                    logger.LogWarning(e.Message);
                }
            }
        }
    }
}
