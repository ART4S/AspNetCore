﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using WebFeatures.QueryFiltering.Exceptions;
using WebFeatures.WebApi.Configuration;
using ValidationException = WebFeatures.Application.Infrastructure.Exceptions.ValidationException;

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

            services.AddControllers();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            //services.AddSwagger();
        }

        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";

                    var responseBody = "Внутренняя ошибка сервера";

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is ValidationException validationEx)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        responseBody = JsonConvert.SerializeObject(validationEx.Fail);
                    }

                    if (exceptionHandlerPathFeature?.Error is FilteringException filteringEx)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        responseBody = filteringEx.Message;
                    }

                    context.Response.WriteAsync(responseBody);
                    return Task.CompletedTask;
                });
            });

            app.UseRouting();

            //app.UseSwagger(); https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1275
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebFeatures v1");
            //});

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
