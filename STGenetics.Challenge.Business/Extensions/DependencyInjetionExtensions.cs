using STGenetics.Challenge.Business.Interfaces;
using STGenetics.Challenge.Business.Services;
using STGenetics.Challenge.Domain.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using AutoMapper;
using MediatR;

namespace STGenetics.Challenge.Business.Extensions
{
    public static class DependencyInjetionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JWT");

            var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value));
            services.Configure<JwtConfiguration>(options =>
            {
                options.Secret = jwtOptions[nameof(JwtConfiguration.Secret)];
                options.AccessTokenExpireTime = int.Parse(jwtOptions[nameof(JwtConfiguration.AccessTokenExpireTime)] ?? "0");
            });

            services.Configure<IdentityOptions>(options =>
            {
                var password = options.Password;
                password.RequireDigit = false;
                password.RequireLowercase = false;
                password.RequireUppercase = false;
                password.RequireNonAlphanumeric = false;
                password.RequiredLength = 6;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secret,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
