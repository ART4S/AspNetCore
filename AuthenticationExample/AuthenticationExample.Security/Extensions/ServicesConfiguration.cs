using AuthenticationExample.Security.Implementations;
using AuthenticationExample.Security.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters()
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
    }
}
