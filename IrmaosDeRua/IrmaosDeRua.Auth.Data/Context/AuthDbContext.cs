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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_config["connectionString"], options =>
            {
                options.MigrationsHistoryTable("__AuthMigrationsHistory");
            });
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<int>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
            builder.Entity<IdentityUserRole<int>>().HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserToken<int>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            builder.Entity<RefreshToken>().HasKey(x => new { x.UserId, x.Token });
            builder.Entity<RefreshToken>().HasOne(x => x.User);
        }
    }
}
