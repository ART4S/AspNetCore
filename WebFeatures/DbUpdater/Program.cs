using DataContext.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.Design;

namespace DbUpdater
{
    static class Program
    {
        static void Main(string[] args)
        {
            var container = new ServiceContainer();
            container.AddService(CreateContext);

            try
            {
                var context = container.GetService<DbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                var logger = container.GetService<ILoggerFactory>().CreateLogger("UpdateDatabase");
                logger.LogWarning(e.Message);
            }
        }

        private static DbContext CreateContext()
        {
            var connectionString = "";
            var opt = new DbContextOptionsBuilder<BaseContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new BaseContext(opt);
            return context;
        }

        public static void AddService<TService>(this ServiceContainer container, Func<TService> serviceProvider)
        {
            container.AddService(typeof(TService), serviceProvider());
        }
    }
}
