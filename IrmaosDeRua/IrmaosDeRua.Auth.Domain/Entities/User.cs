using IrmaosDeRua.Auth.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public short Genre { get; private set; }
        public DateTime Birthday { get; private set; }
        public bool HasVehicle { get; private set; }
        public string ProfilePicture { get; private set; }

        private User() { }

        public User(Email email, PhoneNumber phoneNumber, string firstName, string lastName, 
                    short genre, DateTime birthDay, bool hasVehicle)
        {
            UserName = Email = email.Value;
            FirstName = firstName ?? throw new ArgumentException("The first name can't be empty.");
            LastName = lastName ?? throw new ArgumentException("The last name can't be empty.");
            PhoneNumber = phoneNumber.Value;
            HasVehicle = hasVehicle;

            if (genre < 0 && genre > 1)
                throw new ArgumentException("The genre is invalid.");
            Genre = genre;

            if (birthDay.Year < DateTime.Now.Year - 100 || birthDay > DateTime.Now)
                throw new ArgumentException("The birthday is invalid");
            Birthday = birthDay;
        }
    }
}
