﻿using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Pipeline.Abstractions;
using WebFeatures.Application.Pipeline.Concerns;
using WebFeatures.Application.Pipeline.Mediators;
using WebFeatures.Common.Extensions;
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
        public static void AddAppContext(this IServiceCollection services)
        {
            services.AddDbContext<IAppContext, SqlAppContext>();
        }

        /// <summary>
        /// Добавить обработчики запросов
        /// </summary>
        public static void AddPipeline(this IServiceCollection services)
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
        /// Добавить обработчики для команд
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
        /// Добавить обработчики для запросов
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
        /// Добавить профили automapper
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
        /// Добавить swagger
        /// </summary>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "WebFeatures", Version = "v1" });

                var xml = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                if (File.Exists(xml))
                {
                    c.IncludeXmlComments(xml);
                }
            });
        }
    }
}
