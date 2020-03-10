using IrmaosDeRua.Auth.Domain.Entities;
using IrmaosDeRua.Auth.Domain.Events;
using IrmaosDeRua.Auth.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IrmaosDeRua.Auth.Domain.Subcribers
{
    public class TokenCreatedHandler : INotificationHandler<TokenCreatedEvent>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public TokenCreatedHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(TokenCreatedEvent notification, CancellationToken cancellationToken)
        {
            // The old token is removed, if it exists
            if (!string.IsNullOrEmpty(notification.OldToken))
                await _refreshTokenRepository.Remove(notification.UserId, notification.OldToken);

            // The refresh token is added to the database
            if(notification.NewToken != null)
            {
                var refreshToken = new RefreshToken(notification.UserId, 
                                                    notification.NewToken.RefreshToken, 
                                                    notification.NewToken.RefreshTokenExpiration);
                await _refreshTokenRepository.Add(refreshToken);
            }
        }
    }
}
