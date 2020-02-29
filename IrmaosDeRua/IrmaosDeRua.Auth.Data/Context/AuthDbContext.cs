using IrmaosDeRua.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Data.Context
{
    public class AuthDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly IConfiguration _config;

        public AuthDbContext(IConfiguration config, DbContextOptions<AuthDbContext> options) : base(options)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["connectionString"], options =>
            {
                options.MigrationsHistoryTable("__AuthMigrationsHistory");
            });
        }
    }
}
