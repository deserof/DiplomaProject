using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.ProductionProcesses.Commands.DeleteProductionProcess
{
    public record DeleteProductionProcessCommand(int Id) : IRequest;

    public class DeleteProductionProcessCommandHandler : IRequestHandler<DeleteProductionProcessCommand>
    {
        private readonly IGenericRepository<ProductionProcess> _repository;

        public DeleteProductionProcessCommandHandler(IApplicationDbContext context, IGenericRepository<ProductionProcess> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductionProcessCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
