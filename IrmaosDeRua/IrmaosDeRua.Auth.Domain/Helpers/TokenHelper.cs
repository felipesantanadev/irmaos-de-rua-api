using IrmaosDeRua.Auth.Domain.DTO;
using IrmaosDeRua.Auth.Domain.Entities;
using IrmaosDeRua.Crosscutting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.Helpers
{
    public static class TokenHelper
    {
        public static AuthTokenDTO GenerateToken(User user, string[] roles = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ApplicationSettings.TokenSecret);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            if (roles != null)
                foreach (var role in roles)
                    claims.AddClaim(new Claim(ClaimTypes.Role, role));

            var created = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var generatedToken = tokenHandler.WriteToken(token);

            return new AuthTokenDTO()
            {
                Token = generatedToken,
                RefreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty),
                Roles = roles,
                Created = created,
                Expiration = tokenDescriptor.Expires,
                RefreshTokenExpiration = DateTime.UtcNow.AddMonths(1)
            };
        }
    }
}
