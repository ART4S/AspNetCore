using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Application.Pipeline.Concerns;
using WebFeatures.Application.Pipeline.Mediators;
using WebFeatures.Common.Extensions;
using WebFeatures.Common.Time;
using WebFeatures.DataContext.Sql;

namespace WebFeatures.WebApi.Configuration
{
    /// <summary>
    /// Расширения для конфигурации сервисов приложения
    /// </summary>
    static class ServicesConfiguration
    {
        /// <summary>
        /// Добавить контекст данных
        /// </summary>
        public static void AddAppContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IAppContext, SqlAppContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(nameof(SqlAppContext)));
                opt.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.QueryClientEvaluationWarning));
            });
        }

        /// <summary>
        /// Добавить обработчики запросов
        /// </summary>
        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            services.Scan(scan =>
            {
                var assemblyToScan = AppDomain.CurrentDomain.GetAssembly("WebFeatures.Application");

                scan.FromAssemblies(assemblyToScan)
                    .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();

                scan.FromAssemblies(assemblyToScan)
                    .AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddCommandPipeline();
            services.AddQueryPipeline();
        }

        /// <summary>
        /// Добавить pipeline для команд
        /// </summary>
        private static void AddCommandPipeline(this IServiceCollection services)
        {
            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(SaveChangesHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(ValidationHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(LoggingHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(PerformanceHandlerDecorator<,>));
        }

        /// <summary>
        /// Добавить pipeline для запросов
        /// </summary>
        private static void AddQueryPipeline(this IServiceCollection services)
        {
            services.Decorate(
                typeof(IQueryHandler<,>),
                typeof(ValidationHandlerDecorator<,>));

            services.Decorate(
                typeof(IQueryHandler<,>),
                typeof(LoggingHandlerDecorator<,>));

            services.Decorate(
                typeof(IQueryHandler<,>),
                typeof(PerformanceHandlerDecorator<,>));
        }

        /// <summary>
        /// Добавить валидаторы
        /// </summary>
        public static void AddValidators(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                var assembly = AppDomain.CurrentDomain.GetAssembly("WebFeatures.Application");

                scan.FromAssemblies(assembly)
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
            var config = new MapperConfiguration(opt =>
            {
                var profiles = AppDomain.CurrentDomain.GetAssembly("WebFeatures.Application")
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(Profile)));

                foreach (var profile in profiles)
                {
                    opt.AddProfile(profile);
                }
            });

            config.AssertConfigurationIsValid();

            services.AddSingleton(provider => config.CreateMapper());
        }

        /// <summary>
        /// Добавить инфраструктурные сервисы приложения
        /// </summary>
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IServerTime, ServerTime>();
        }
    }
}
