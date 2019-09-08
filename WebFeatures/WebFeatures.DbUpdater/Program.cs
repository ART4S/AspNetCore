using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common.Time;
using WebFeatures.DataContext.Sql;
using WebFeatures.DbUpdater.Core;

namespace WebFeatures.DbUpdater
{
    class Program
    {
        public static void Main()
        {
            var provider = BuildServices();
            var updater = provider.GetService<Updater>();

            updater.UpdateDb();
        }

        private static ServiceProvider BuildServices()
        {
            var services = new ServiceCollection();
            var configuration = BuildConfiguration();

            services.AddLogging(x => x.AddConsole());

            services.AddSingleton(configuration);
            services.AddSingleton<ITimeProvider, TimeProvider>();

            services.AddDbContext<IAppContext, SqlAppContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Sql")));

            services.AddOptions();
            services.Configure<UpdaterOptions>(configuration.GetSection(nameof(UpdaterOptions)));

            services.AddSingleton<Updater>();

            return services.BuildServiceProvider();
        }

        private static IConfiguration BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration;
        }
    }
}
