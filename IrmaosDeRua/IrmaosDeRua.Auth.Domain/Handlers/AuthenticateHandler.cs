using CSharpFunctionalExtensions;
using IrmaosDeRua.Auth.Domain.Commands;
using IrmaosDeRua.Auth.Domain.DTO;
using IrmaosDeRua.Auth.Domain.Entities;
using IrmaosDeRua.Auth.Domain.Events;
using IrmaosDeRua.Auth.Domain.Helpers;
using IrmaosDeRua.Auth.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IrmaosDeRua.Auth.Domain.Handlers
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, Result<AuthTokenDTO>>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticateHandler(IMediator mediator, UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository)
        {
            _mediator = mediator;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<AuthTokenDTO>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return Result.Failure<AuthTokenDTO>("Invalid username or password.");

            if (request.GrantType.Equals("password"))
            {
                var isPassowrdValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isPassowrdValid)
                    return Result.Failure<AuthTokenDTO>("Invalid username or password.");

                var roles = await _userManager.GetRolesAsync(user);
                var token = TokenHelper.GenerateToken(user, roles.ToArray());

                await _mediator.Publish(new TokenCreatedEvent(user.Id, token), cancellationToken);

                return Result.Ok<AuthTokenDTO>(token);
            }
            else if (request.GrantType.Equals("refresh_token"))
            {
                var refreshTokenResult = _refreshTokenRepository.CheckRefreshToken(user.Id, request.RefreshToken);
                if (refreshTokenResult.IsFailure)
                    return Result.Failure<AuthTokenDTO>(refreshTokenResult.Error);

                var roles = await _userManager.GetRolesAsync(user);
                var token = TokenHelper.GenerateToken(user, roles.ToArray());

                await _mediator.Publish(new TokenCreatedEvent(user.Id, token, oldToken: request.RefreshToken), cancellationToken);

                return Result.Ok<AuthTokenDTO>(token);
            }
            else
                return Result.Failure<AuthTokenDTO>("Invalid Grant Type.");
        }
    }
}
