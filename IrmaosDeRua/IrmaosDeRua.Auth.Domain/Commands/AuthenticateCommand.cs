using CSharpFunctionalExtensions;
using IrmaosDeRua.Auth.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.Commands
{
    public class AuthenticateCommand : IRequest<Result<AuthTokenDTO>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
        public string RefreshToken { get; set; }
    }
}
