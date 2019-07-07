using DapperContext.Repositories.Npgsql;
using DapperContext.Repositories.Sql;
using EFContext;
using EFContext.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Abstractions;
using Model.Entities;
using Npgsql;
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
        public static IServiceCollection AddEFContextSql(this IServiceCollection services, IConfiguration configuration)
        {
            var cnn = configuration.GetConnectionString("SqlConnection");

            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(cnn));
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

            return services;
        }

        /// <summary>
        /// Добавить сервисы Dapper для SQL
        /// </summary>
        public static IServiceCollection AddDapperSql(this IServiceCollection services, IConfiguration configuration)
        {
            var cnn = configuration.GetConnectionString("SqlConnection");

            services.AddTransient<IDbConnection>(x => new SqlConnection(cnn));
            services.AddTransient<IRepository<Customer>, CustomerSqlRepository>();
            services.AddTransient<IRepository<Order>, OrderSqlRepository>();
            services.AddTransient<IRepository<Product>, ProductSqlRepository>();

            return services;
        }

        /// <summary>
        /// Добавить сервисы Dapper для Npgsql
        /// </summary>
        public static IServiceCollection AddDapperNpgsql(this IServiceCollection services, IConfiguration configuration)
        {
            var cnn = configuration.GetConnectionString("NpgsqlConnection");

            services.AddTransient<IDbConnection>(x => new NpgsqlConnection(cnn));
            services.AddTransient<IRepository<Customer>, CustomerNpgsqlRepository>();
            services.AddTransient<IRepository<Order>, OrderNpgsqlRepository>();
            services.AddTransient<IRepository<Product>, ProductNpgsqlRepository>();

            return services;
        }
    }
}
