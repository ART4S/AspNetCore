using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using Web.Decorators.Abstractions;
using Web.Decorators.Implementations;
using Web.Features.Authentication;
using Web.Features.Registration;
using Web.Infrastructure;

namespace Web.Extensions
{
    /// <summary>
    /// Расширения для конфигурации сервисов приложения
    /// </summary>
    public static class ServicesConfigurationExtensions
    {
        /// <summary>
        /// Добавить контекст данных Sql
        /// </summary>
        public static void AddSqlDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Sql");

            services.AddDbContext<DbContext, DataContext.Sql.BaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        /// <summary>
        /// Добавить обработчики запросов
        /// </summary>
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IHandler<LoginCommand, Result<Claim[], string>>>(x =>
            {
                var dbContext = x.GetRequiredService<DbContext>();
                var protector = x.GetRequiredService<IDataProtectionProvider>();
                var logger = x.GetRequiredService<ILogger<LoginCommandHandler>>();

                return new SaveChangesDecorator<LoginCommand, Result<Claim[], string>>(
                    new LoggerDecorator<LoginCommand, Result<Claim[], string>>(
                        new LoginCommandHandler(protector, dbContext), logger),
                    dbContext);
            });

            services.AddScoped<IHandler<RegisterUserCommand, Result>>(x =>
            {
                var dbContext = x.GetRequiredService<DbContext>();
                var protector = x.GetRequiredService<IDataProtectionProvider>();
                var mapper = x.GetRequiredService<IMapper>();
                var logger = x.GetRequiredService<ILogger<RegisterUserCommandHandler>>();

                return new SaveChangesDecorator<RegisterUserCommand, Result>(
                    new LoggerDecorator<RegisterUserCommand, Result>(
                        new RegisterUserCommandHandler(protector, dbContext, mapper), logger), 
                    dbContext);
            });
        }

        /// <summary>
        /// Добавить профили Automapper
        /// </summary>
        public static void AddAutomapperProfiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(opt =>
            {
                var profiles = typeof(Startup).Assembly
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
