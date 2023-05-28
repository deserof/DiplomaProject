using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using Manufacturing.Domain.Events;
using MediatR;

namespace Manufacturing.Application.Details.Commands.CreateDetail
{
    public record CreateDetailCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateDetailCommandHandler : IRequestHandler<CreateDetailCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = new Detail
            {
                Name = request.Name,
            };

            entity.AddDomainEvent(new DetailCreatedEvent(entity));
            _context.Details.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
