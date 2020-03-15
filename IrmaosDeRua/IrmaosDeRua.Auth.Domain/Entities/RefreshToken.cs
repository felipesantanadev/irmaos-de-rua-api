using IrmaosDeRua.Crosscutting.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.Entities
{
    public class RefreshToken
    {
        public RefreshToken(int userId, string token, DateTime? expired)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user.");
            UserId = userId;

            Token = token ?? throw new ArgumentException("Invalid token.");
            Expired = expired;
            Created = DateTime.UtcNow;
        }

        public int UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime? Expired { get; private set; }
        public DateTime Created { get; private set; }

        public virtual User User { get; private set; }
    }
}
