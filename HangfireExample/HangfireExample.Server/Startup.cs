using Hangfire;
using Hangfire.SqlServer;
using HangfireExample.Server.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HangfireExample.Server
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
            services.AddHangfire(configuration =>
            {
                configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.GetValue<string>("Data:ConnectionStrings:Hangfire"), new SqlServerStorageOptions()
                    {
                        PrepareSchemaIfNecessary = true,
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        UsePageLocksOnDequeue = true,
                        DisableGlobalLocks = true
                    });
            });

            services.AddHangfireServer(options =>
            {
                options.Queues = new[] { "recurring", "email" };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = null,
                Authorization = new []{ new AuthorizationFilter() }
            });

            app.UseHangfireDashboard("/hangfireSafe", new DashboardOptions()
            {
                AppPath = null,
                IsReadOnlyFunc = ctx => true
            });

            app.UseHangfireServer();
        }
    }
}
