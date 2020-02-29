using IrmaosDeRua.Auth.Data.Context;
using IrmaosDeRua.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Data.Extensions
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
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthDbContext>();
        }
    }
}
