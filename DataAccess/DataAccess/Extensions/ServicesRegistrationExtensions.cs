using DapperContext.Repositories;
using DapperContext.StoredProcedureProviders.Implementations;
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
    public static class ServicesRegistrationExtensions
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

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();

            services.AddTransient<OrderStoredProcedureProvider>();
            services.AddTransient<ProductStoredProcedureProvider>();
            services.AddTransient<CustomersStoredProcedureProvider>();

            return services;
        }

        /// <summary>
        /// Добавить сервисы Dapper для Npgsql
        /// </summary>
        public static IServiceCollection AddDapperNpgsql(this IServiceCollection services, IConfiguration configuration)
        {
            var cnn = configuration.GetConnectionString("NpgsqlConnection");
            services.AddTransient<IDbConnection>(x => new NpgsqlConnection(cnn));

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();

            return services;
        }
    }
}
