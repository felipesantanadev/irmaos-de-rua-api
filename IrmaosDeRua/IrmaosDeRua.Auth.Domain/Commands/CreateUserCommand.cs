using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.Commands
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string PhoneDDD { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Genre { get; set; }
        public DateTime Birthday { get; set; }
        public bool HasVehicle { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
