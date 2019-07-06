using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authentication.Extensions
{
    /// <summary>
    /// Расширения для работы с хостом
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Принять миграции в бд
        /// </summary>
        public static IWebHost MigrateDb(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // log error
                }
            }

            return host;
        }
    }
}
