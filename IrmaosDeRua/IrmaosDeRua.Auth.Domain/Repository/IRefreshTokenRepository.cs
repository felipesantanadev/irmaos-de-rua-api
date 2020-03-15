using CSharpFunctionalExtensions;
using IrmaosDeRua.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IrmaosDeRua.Auth.Domain.Repository
{
    public interface IRefreshTokenRepository
    {
        Task<Result> Add(RefreshToken refreshToken);
        Task<Result> Remove(int userId, string refreshToken);
        Result CheckRefreshToken(int userId, string refreshToken);
    }
}
