﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.Events
{
    public class UserCreatedEvent : INotification
    {
        public string Email { get; }

        public UserCreatedEvent(string email)
        {
            Email = email;
        }
    }
}
