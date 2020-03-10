using CSharpFunctionalExtensions;
using IrmaosDeRua.Auth.Shared.Extensions;
using IrmaosDeRua.Auth.Domain.Commands;
using IrmaosDeRua.Auth.Domain.Entities;
using IrmaosDeRua.Auth.Domain.Events;
using IrmaosDeRua.Auth.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IrmaosDeRua.Auth.Domain.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        public CreateUserHandler(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(!request.Password.Equals(request.PasswordConfirmation))
                return Result.Failure("The password and the password confirmation doesn't match.");

            var email = Email.Create(request.Email);
            if (!email.IsSuccess)
                return Result.Failure(email.Error);

            var phoneNumber = PhoneNumber.Create(request.PhoneDDD, request.PhoneNumber);
            if (!phoneNumber.IsSuccess)
                return Result.Failure(phoneNumber.Error);

            var user = new User(email.Value, phoneNumber.Value, request.FirstName, request.LastName, 
                                request.Genre, request.Birthday, request.HasVehicle);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Result.Failure(result.Errors.GetErrorString());

            await _mediator.Publish(new UserCreatedEvent(user.Email), cancellationToken);

            return Result.Ok();
        }
    }
}
