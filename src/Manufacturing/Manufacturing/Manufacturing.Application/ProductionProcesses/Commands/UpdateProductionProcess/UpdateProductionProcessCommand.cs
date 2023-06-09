using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.ProductionProcesses.Commands.UpdateProductionProcess
{
    public record UpdateProductionProcessCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class UpdateProductionProcessCommandHandler : IRequestHandler<UpdateProductionProcessCommand>
    {
        private readonly IGenericRepository<ProductionProcess> _repository;

        public UpdateProductionProcessCommandHandler(IGenericRepository<ProductionProcess> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductionProcessCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            entity.Id = request.Id;
            entity.Name = request.Name;
            entity.Description = request.Description;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
