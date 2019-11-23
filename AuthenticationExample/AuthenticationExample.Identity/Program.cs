using AuthenticationExample.Identity.Data.Model;
using AuthenticationExample.Identity.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Security.Claims;

namespace AuthenticationExample.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.SeedInitialData();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class HostExtensions
    {
        public static void SeedInitialData(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            var admin = new User() { UserName = "admin" };
            userManager.CreateAsync(admin, "admin").GetAwaiter().GetResult();

            userManager.AddClaimsAsync(
                admin, 
                new []
                {
                    new Claim(ClaimTypes.Role, SystemRoles.Admin),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.Add(TimeSpan.FromDays(1)).ToString(CultureInfo.InvariantCulture))
                }).GetAwaiter().GetResult();

            var user = new User() { UserName = "user" };
            userManager.CreateAsync(user, "user").GetAwaiter().GetResult();

            userManager.AddClaimsAsync(
                user, 
                new[]
                {
                    new Claim(ClaimTypes.Role, SystemRoles.User), 
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.Add(TimeSpan.FromDays(1)).ToString(CultureInfo.InvariantCulture))
                }).GetAwaiter().GetResult();
        }
    }
}
