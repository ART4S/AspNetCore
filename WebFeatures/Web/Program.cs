using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Web.Extensions;

namespace Web
{
    /// <summary>
    /// Стартовый класс приложения
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            host.UpdateDatabase();
            host.Run();
        }

        /// <summary>
        /// Конфигурация хоста
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
