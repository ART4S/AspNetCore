using DapperContext;
using DapperContext.QueryProvider.Abstractions;
using EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Abstractions;
using System.Data;
using System.Data.SqlClient;
using DapperContext.QueryProvider.Implementations;

namespace DataAccess.Extensions
{
    /// <summary>
    /// Расширения для регистрации сервисов приложения
    /// </summary>
    public static class ServicesRegistrationExtension
    {
        public static IServiceCollection AddEFContextSql(this IServiceCollection services, IConfiguration config)
        {
            var cnn = config.GetConnectionString("SqlConnection");

            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(cnn));
            services.AddTransient(typeof(IRepository<>), typeof(BaseEFRepository<>));

            return services;
        }

        public static IServiceCollection AddDapperSql(this IServiceCollection services, IConfiguration config)
        {
            var cnn = config.GetConnectionString("SqlConnection");

            services.AddTransient(typeof(IRepository<>), typeof(BaseDapperRepository<>));
            services.AddTransient(typeof(IDbConnection), x => new SqlConnection(cnn));
            services.AddTransient(typeof(IQueryProvider), typeof(SqlQueryProvider));
            services.AddTransient(typeof(TableInfoProvider<>));

            return services;
        }
    }
}
