using AutoMapper;
using DataContext.Sql;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Web.Infrastructure.Mediators;
using Web.Infrastructure.Pipeline.Abstractions;
using Web.Infrastructure.Pipeline.Implementations;

namespace Web.Startup.Configuration
{
    /// <summary>
    /// Расширения для конфигурации сервисов приложения
    /// </summary>
    public static class ServicesConfigurationExtensions
    {
        /// <summary>
        /// Добавить контекст данных
        /// </summary>
        public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Sql");
            services.AddDbContext<DbContext, BaseContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });
        }

        /// <summary>
        /// Добавить обработчики запросов
        /// </summary>
        public static void AddRequestHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            services.Scan(scan =>
            {
                scan.FromCallingAssembly()
                    .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();

                scan.FromCallingAssembly()
                    .AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(SaveChangesHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(LoggingHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(ValidationHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(PerformanceHandlerDecorator<,>));
        }

        /// <summary>
        /// Добавить валидаторы
        /// </summary>
        public static void AddValidators(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblyOf<Startup>()
                    .AddClasses(x => x.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
        }

        /// <summary>
        /// Добавить профили Automapper
        /// </summary>
        public static void AddAutomapper(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(opt =>
                {
                    var profiles = Assembly.GetCallingAssembly()
                        .GetTypes()
                        .Where(x => x.IsSubclassOf(typeof(Profile)));

                    foreach (var profile in profiles)
                    {
                        opt.AddProfile(profile);
                    }
                });

                return config.CreateMapper();
            });
        }
    }
}
