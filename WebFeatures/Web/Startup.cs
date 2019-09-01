using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Web.Configuration;
using Web.Infrastructure.Validation.Attributes;

namespace Web
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
            services.AddSqlDataContext(Configuration);
            services.AddHandlers();
            services.AddAutomapperProfiles();
            services.AddDataProtection();

            services.AddMvc(opt =>
            {
                opt.Filters.Add(new ValidateModelStateAttribute());
                opt.Filters.Add(new ProducesAttribute("application/json"));
                opt.Filters.Add(new AuthorizeFilter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

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
