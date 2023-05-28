using Manufacturing.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Manufacturing.Application.Details.EventHandlers
{
    public class DetailCompletedEventHandler : INotificationHandler<DetailCompletedEvent>
    {
        private readonly ILogger<DetailCompletedEventHandler> _logger;

        public DetailCompletedEventHandler(ILogger<DetailCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DetailCompletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
