using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using Web.Abstractions;
using Web.Features.Authentication;
using Web.Features.Registration;
using Web.Infrastructure;
using Web.PipelineDecorators;

namespace Web.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddSqlDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            services.AddDbContext<DbContext, DataContext.Sql.BaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IHandler<Login, Result<Claim[], string>>>(x =>
            {
                var dbContext = x.GetRequiredService<DbContext>();
                var protector = x.GetRequiredService<IDataProtectionProvider>();

                return new SaveChangesDecorator<Login, Result<Claim[], string>>(
                    new LoginHandler(protector, dbContext), dbContext);
            });

            services.AddScoped<IHandler<RegisterUser, Result>>(x =>
            {
                var dbContext = x.GetRequiredService<DbContext>();
                var protector = x.GetRequiredService<IDataProtectionProvider>();
                var mapper = x.GetRequiredService<IMapper>();

                return new SaveChangesDecorator<RegisterUser, Result>(
                    new RegisterUserHandler(protector, dbContext, mapper), dbContext);
            });
        }

        public static void AddAutomapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(opt =>
            {
                var profiles = typeof(Program).Assembly
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(Profile)));

                foreach (var profile in profiles)
                {
                    opt.AddProfile(profile);
                }
            });

            services.AddSingleton(x => config.CreateMapper());
        }
    }
}
