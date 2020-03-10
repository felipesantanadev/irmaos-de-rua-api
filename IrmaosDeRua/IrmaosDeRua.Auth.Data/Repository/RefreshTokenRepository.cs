using CSharpFunctionalExtensions;
using IrmaosDeRua.Auth.Data.Context;
using IrmaosDeRua.Auth.Domain.Entities;
using IrmaosDeRua.Auth.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrmaosDeRua.Auth.Data.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthDbContext _context;

        public RefreshTokenRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Add(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Add(refreshToken);
                var result = await _context.SaveChangesAsync();

                return result > 0
                       ? Result.Ok()
                       : Result.Failure("There was an error to add the refresh token.");
            }
            catch (Exception ex)
            {
                return Result.Failure("There was an internal error to create the refresh token");
            }
        }

        public async Task<Result> Remove(int userId, string refreshToken)
        {
            try
            {
                var token = _context.RefreshTokens.Where(x => x.UserId == userId
                                               && x.Token.Equals(refreshToken)).FirstOrDefault();

                if (token == null)
                    return Result.Failure("Refresh token not found.");

                _context.RefreshTokens.Remove(token);
                var result = await _context.SaveChangesAsync();

                return result > 0
                       ? Result.Ok()
                       : Result.Failure("There was an error to remove the refresh token.");
            }
            catch (Exception ex)
            {
                return Result.Failure("There was an internal error to remove the refresh token");
            }
        }

        public Result CheckRefreshToken(int userId, string refreshToken)
        {
            try
            {
                var valid = _context.RefreshTokens.Any(x => x.UserId == userId
                                               && x.Token.Equals(refreshToken)
                                               && x.Expired > DateTime.UtcNow);

                return valid ? Result.Ok() : Result.Failure("Invalid refresh token.");
            }
            catch (Exception ex)
            {
                return Result.Failure("There was an internal error to check the refresh token");
            }
        }
    }
}
