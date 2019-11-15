using AuthenticationExample.Data;
using AuthenticationExample.Security.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

[assembly: ApiController]
namespace AuthenticationExample.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>();
            services.AddTokenAuthentication();

            services.AddControllers(configure =>
            {
                configure.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            // who are you?
            app.UseAuthentication();

            // are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
