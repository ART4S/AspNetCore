using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Web.Infrastructure.Exceptions;
using Web.Startup.Configuration;

namespace Web.Startup
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
        /// Настройка сервисов приложения
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataContext(Configuration);
            services.AddRequestHandlers();
            services.AddValidators();
            services.AddAutomapper();
            services.AddDataProtection();

            services.AddMvc(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter());
                opt.Filters.Add(new ProducesAttribute("application/json"));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info(){Title = "WebFeatures", Version = "v1"});

                var xml = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                if (File.Exists(xml))
                {
                    c.IncludeXmlComments(xml);
                }
            });
        }

        /// <summary>
        /// Настройка приложения
        /// </summary>
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/plain";

                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (feature?.Error is ResultException resultEx)
                    {
                        context.Response.StatusCode = resultEx.Result.StatusCode;
                        context.Response.ContentType = "application/json";

                        var json = JsonConvert.SerializeObject(resultEx.Result);
                        context.Response.WriteAsync(json);

                        return Task.CompletedTask;
                    }

                    context.Response.WriteAsync("Неизвестная ошибка");
                    return Task.CompletedTask;
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebFeatures v1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
