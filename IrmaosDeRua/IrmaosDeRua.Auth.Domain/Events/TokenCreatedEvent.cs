using IrmaosDeRua.Auth.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.Events
{
    public class TokenCreatedEvent : INotification
    {
        public TokenCreatedEvent(int userId, AuthTokenDTO newToken, string oldToken = "")
        {
            UserId = userId;
            NewToken = newToken;
            OldToken = oldToken;
        }

        public int UserId { get; private set; }
        public AuthTokenDTO NewToken { get; private set; }
        public string OldToken { get; private set; }
    }
}
