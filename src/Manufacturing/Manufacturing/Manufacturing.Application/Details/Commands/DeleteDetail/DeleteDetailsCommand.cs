using Manufacturing.Application.Common.Exceptions;
using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using Manufacturing.Domain.Events;
using MediatR;

namespace Manufacturing.Application.Details.Commands.DeleteDetail
{
    public record DeleteDetailsCommand(int Id) : IRequest;

    public class DeleteDetailCommandHandler : IRequestHandler<DeleteDetailsCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteDetailsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Details.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Detail), request.Id);
            }

            _context.Details.Remove(entity);
            entity.AddDomainEvent(new DetailDeletedEvent(entity));
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
