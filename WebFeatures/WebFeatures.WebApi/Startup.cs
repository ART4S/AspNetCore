using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.WebApi.Configuration;
using WebFeatures.WebApi.Middlewares;

namespace WebFeatures.WebApi
{
    /// <summary>
    /// Конфигуратор приложения
    /// </summary>
    public class Startup
    {
        /// <inheritdoc />
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Настройки приложения
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Конфигурация сервисов приложения
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppContext();
            services.AddPipeline();
            services.AddValidators();
            services.AddAutomapper();
            services.AddDataProtection();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddSwagger();
        }

        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebFeatures v1");
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
