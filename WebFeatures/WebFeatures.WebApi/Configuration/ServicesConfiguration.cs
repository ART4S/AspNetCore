﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Application.Pipeline.Implementations;
using WebFeatures.Common.Extensions;
using WebFeatures.DataContext.Sql;

namespace WebFeatures.WebApi.Configuration
{
    /// <summary>
    /// Расширения для конфигурации сервисов приложения
    /// </summary>
    public static class ServicesConfiguration
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
                var assembly = AppDomain.CurrentDomain.GetAssembly("WebFeatures.Application");

                scan.FromAssemblies(assembly)
                    .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();

                scan.FromAssemblies(assembly)
                    .AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddCommandPipeline();
            //services.AddQueryPipeline();
        }

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
            services.AddSingleton(provider =>
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

                return config.CreateMapper();
            });
        }
    }
}