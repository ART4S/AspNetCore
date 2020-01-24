using AuthenticationExample.Identity.Data;
using AuthenticationExample.Identity.Data.Model;
using AuthenticationExample.Identity.Security;
using AuthenticationExample.Identity.Security.Requirements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AuthenticationExample.Identity
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
            services.AddControllers();

            services.AddDbContext<UserContext>(options =>
            {
                options.UseInMemoryDatabase("Memory");
            });

            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;

                    options.ClaimsIdentity.UserIdClaimType = SystemClaimTypes.Id;

                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                    //options.SignIn.RequireConfirmedAccount = true;
                    //options.SignIn.RequireConfirmedEmail = true;

                    //options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "AuthenticationExample.Cookie";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    SystemPolicies.MinimumYearWorked,
                    policyOptions =>
                    {
                        policyOptions.AddRequirements(new MinimumYearWorkedRequirement(2));
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
