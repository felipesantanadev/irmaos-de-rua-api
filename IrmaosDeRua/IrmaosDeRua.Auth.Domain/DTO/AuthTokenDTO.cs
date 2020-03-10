using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.DTO
{
    public class AuthTokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public string[] Roles { get; set; }
    }
}
