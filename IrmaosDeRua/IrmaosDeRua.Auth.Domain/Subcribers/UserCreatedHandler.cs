using IrmaosDeRua.Auth.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IrmaosDeRua.Auth.Domain.Subcribers
{
    public class UserCreatedHandler : INotificationHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Send email confirmation
        }
    }
}
