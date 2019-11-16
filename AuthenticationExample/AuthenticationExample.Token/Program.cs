using AuthenticationExample.Token.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace AuthenticationExample.Token
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetService<UserContext>();
                context.SeedData();
            }
            catch (Exception e)
            {
                var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
                logger.LogError(e, "Не удалось заполнить пользовательский контекст данными");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
