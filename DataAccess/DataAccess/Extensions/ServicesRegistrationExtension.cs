using DapperContext.Repositories;
using EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Abstractions;
using Model.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Extensions
{
    /// <summary>
    /// Расширения для регистрации сервисов приложения
    /// </summary>
    public static class ServicesRegistrationExtension
    {
        /// <summary>
        /// Добавить сервисы EF для SQL
        /// </summary>
        public static IServiceCollection AddEFContextSql(this IServiceCollection services, IConfiguration config)
        {
            var cnn = config.GetConnectionString("SqlConnection");

            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(cnn));
            services.AddTransient(typeof(IRepository<>), typeof(EFContext.Repositories.BaseRepository<>));

            return services;
        }

        /// <summary>
        /// Добавить сервисы Dapper для SQL
        /// </summary>
        public static IServiceCollection AddDapperSql(this IServiceCollection services, IConfiguration config)
        {
            var cnn = config.GetConnectionString("SqlConnection");
            services.AddTransient(typeof(IDbConnection), x => new SqlConnection(cnn));

            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();

            return services;
        }
    }
}
