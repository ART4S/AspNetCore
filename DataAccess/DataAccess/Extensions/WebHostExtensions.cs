using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.IO;
using System.Linq;
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
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetService<AppContext>().Database;
                    db.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = host.Services.GetService<ILoggerFactory>().CreateLogger<AppContext>();
                    logger.LogCritical(ex, "Ошибка при попытке принятия миграции");
                }
            }

            return host;
        }

        /// <summary>
        /// Создать хранимые процедуры для sql
        /// </summary>
        public static IWebHost CreateSqlStoredProcedures(this IWebHost host)
        {
            var scriptsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts\\Sql\\");
            CreateStoredProcedures(host, scriptsDir);

            return host;
        }

        /// <summary>
        /// Создать хранимые процедуры для Postgree
        /// </summary>
        public static IWebHost CreateNpgsqlStoredProcedures(this IWebHost host)
        {
            var scriptsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts\\Npgsql\\");
            CreateStoredProcedures(host, scriptsDir);

            return host;
        }

        /// <summary>
        /// Создать хранимые процедуры
        /// </summary>
        private static void CreateStoredProcedures(IWebHost host, string scriptsDir)
        {
            var scripts = Directory
                .GetFiles(scriptsDir, "*.sql", SearchOption.AllDirectories)
                .Select(File.ReadAllText);

            using (var connection = host.Services.GetService<IDbConnection>())
            {
                connection.Open();

                foreach (var script in scripts)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = script;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
