using Manufacturing.Application.Common.Exceptions;
using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Details.Commands.UpdateDetail
{
    public record UpdateDetailCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateDetailCommandHandler : IRequestHandler<UpdateDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Details
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Detail), request.Id);
            }

            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
