using AuthenticationExample.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace AuthenticationExample.WebApi.Extensions
{
    internal static class HostExtensions
    {
        public static void SeedUserContext(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetService<UserContext>();
                context.SeedData();
            }
            catch (Exception e)
            {
                var logger = scope.ServiceProvider.GetService<ILoggerFactory>().CreateLogger("SeedUserContext");
                logger.LogError(e, "Не удалось заполнить пользовательский контекст данными");
            }
        }
    }
}
