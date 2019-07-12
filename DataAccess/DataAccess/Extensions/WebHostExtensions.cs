using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Npgsql;
using AppContext = EFContext.AppContext;

namespace DataAccess.Extensions
{
    /// <summary>
    /// Расширения для работы с WebHost
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Загрузить обновленные данные в БД
        /// </summary>
        public static IWebHost MigrateDbChanges(this IWebHost host)
        {
            try
            {
                var db = host.Services.GetService<AppContext>().Database;
                db.Migrate();
            }
            catch (Exception ex)
            {
                var logger = host.Services.GetService<ILoggerFactory>().CreateLogger<AppContext>();
                logger.LogCritical(ex, "Ошибка при попытке принятия миграции");
            }

            return host;
        }

        /// <summary>
        /// Создать хранимые процедуры для sql
        /// </summary>
        public static IWebHost CreateSqlStoredProcedures(this IWebHost host)
        {
            var connectionSting = host.Services
                .GetService<IConfiguration>()
                .GetConnectionString("SqlConnection");

            var scripts = Directory
                .GetFiles(
                    path: Path.Combine(Directory.GetCurrentDirectory(), "DapperContext\\Scripts\\"),
                    searchPattern: " *.sql",
                    searchOption: SearchOption.AllDirectories)
                .Select(File.ReadAllText);

            using (var connection = new NpgsqlConnection(connectionSting))
            {
                foreach (var script in scripts)
                {
                    using (var command = new NpgsqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            return host;
        }

        /// <summary>
        /// Создать хранимые процедуры для Postgree
        /// </summary>
        public static IWebHost CreateNpgsqlStoredProcedures(this IWebHost host)
        {
            var connectionSting = host.Services
                .GetService<IConfiguration>()
                .GetConnectionString("NpgsqlConnection");

            var scripts = Directory
                .GetFiles(
                    path: Path.Combine(Directory.GetCurrentDirectory(), "DapperContext\\Scripts\\"),
                    searchPattern: " *.sql",
                    searchOption: SearchOption.AllDirectories)
                .Select(File.ReadAllText);
                
            using (var connection = new NpgsqlConnection(connectionSting))
            {
                foreach (var script in scripts)
                {
                    using (var command = new NpgsqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

            return host;
        }
    }
}
