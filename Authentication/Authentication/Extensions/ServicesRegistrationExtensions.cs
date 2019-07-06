using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Implementations;
using Security.Interfaces;
using System;
using System.Linq;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Authentication.Extensions
{
    /// <summary>
    /// Сервисы для регистрации зависимостей
    /// </summary>
    public static class ServicesRegistrationExtensions
    {
        /// <summary>
        /// Добаваить контекст доступа к данным пользователей
        /// </summary>
        public static IServiceCollection AddUserContext(this IServiceCollection services, IConfiguration configuration)
        {
            var cnn = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(cnn));

            return services;
        }

        /// <summary>
        /// Добавить аутентификацию на основе куки
        /// </summary>
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>))
                .AddScoped<IAuthService, AuthService>()
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            return services;
        }

        /// <summary>
        /// Добавить профили маппера
        /// </summary>
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            var profiles = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(Profile)) && !x.Name.StartsWith("AutoMapper")));

            var config = new MapperConfiguration(x => x.AddMaps(profiles));

            services.AddSingleton<IConfigurationProvider>(config);
            services.AddSingleton<IMapper, Mapper>();

            return services;
        }
    }
}
