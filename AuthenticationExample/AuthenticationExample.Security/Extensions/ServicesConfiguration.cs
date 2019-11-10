using AuthenticationExample.Security.Implementations;
using AuthenticationExample.Security.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace AuthenticationExample.Security.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddTokenAuthentication(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthenticationDefaults.SecurityKey,

                    ValidateIssuer = true,
                    ValidIssuer = AuthenticationDefaults.Issuer,

                    ValidateAudience = true,
                    ValidAudience = AuthenticationDefaults.Audience, // when we are using token in other app, put other app URL

                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddCookieAuthentication(this IServiceCollection services)
        {
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();
        }
    }
}
