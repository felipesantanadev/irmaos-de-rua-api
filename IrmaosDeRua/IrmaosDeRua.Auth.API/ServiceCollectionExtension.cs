using IrmaosDeRua.Auth.Data.Context;
using IrmaosDeRua.Auth.Data.Repository;
using IrmaosDeRua.Auth.Domain.Entities;
using IrmaosDeRua.Auth.Domain.Repository;
using IrmaosDeRua.Crosscutting;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.API
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureAuthContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(configuration["connectionString"],
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly("IrmaosDeRua.Auth.Data");
                    });
            });

            services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthDbContext>();

            var assembly = AppDomain.CurrentDomain.Load("IrmaosDeRua.Auth.Domain");
            services.AddMediatR(assembly);

            var key = Encoding.ASCII.GetBytes(ApplicationSettings.TokenSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero,
                            ValidateLifetime = true
                        };
                    });

            // Enable the token as a form to authorize the access to resources from this project
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
