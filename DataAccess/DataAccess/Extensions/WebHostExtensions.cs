﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using AppContext = EFContext.AppContext;

namespace DataAccess.Extensions
{
    /// <summary>
    /// Расширения для работы с WebHost
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Накатить миграции в БД
        /// </summary>
        public static IWebHost MigrateDB(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                try
                {
                    var db = serviceProvider.GetService<AppContext>().Database;
                    db.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<AppContext>();
                    logger.LogCritical(ex, "Ошибка при попытке накатить миграцию");
                }
            }
            return host;
        }
    }
}
