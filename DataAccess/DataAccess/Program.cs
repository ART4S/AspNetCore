using DataAccess.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DataAccess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            host.MigrateDbChanges();
            host.CreateSqlStoredProcedures();
            host.CreateSqlStoredProcedures();

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
            => WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
