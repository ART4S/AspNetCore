using AuthenticationExample.Data;
using AuthenticationExample.Security.Implementations;
using AuthenticationExample.Security.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationExample.WebApi.Extensions
{
    internal static class ServicesConfiguration
    {
        public static void AddUserContext(this IServiceCollection services)
        {
            services.AddDbContext<UserContext>();
        }

        public static void AddTokenAuthentication(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
