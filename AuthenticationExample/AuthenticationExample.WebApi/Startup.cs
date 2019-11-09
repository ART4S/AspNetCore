using AuthenticationExample.Security.Middlewares;
using AuthenticationExample.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
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
            services.AddUserContext();
            services.AddTokenAuthentication();

            services.AddControllers(configure =>
            {
                configure.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<TokenMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
