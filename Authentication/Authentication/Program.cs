using Authentication.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

            host.MigrateDb();
            host.Run();
        }
    }
}
