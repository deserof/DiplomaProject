using Manufacturing.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Manufacturing.Application.Details.EventHandlers
{
    public class DetailCreatedEventHandler : INotificationHandler<DetailCreatedEvent>
    {
        private readonly ILogger<DetailCreatedEventHandler> _logger;

        public DetailCreatedEventHandler(ILogger<DetailCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DetailCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
