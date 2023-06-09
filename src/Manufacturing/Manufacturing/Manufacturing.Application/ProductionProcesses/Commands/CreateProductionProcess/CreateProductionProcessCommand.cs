using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.ProductionProcesses.Commands.CreateProductionProcess
{
    public record CreateProductionProcessCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; } = TimeSpan.FromHours(1);
    }

    public class CreateProductionProcessCommandHandler : IRequestHandler<CreateProductionProcessCommand, int>
    {
        private readonly IGenericRepository<ProductionProcess> _repository;

        public CreateProductionProcessCommandHandler(IGenericRepository<ProductionProcess> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProductionProcessCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProductionProcess()
            {
                Name = request.Name,
                Description = request.Description,
                Duration = request.Duration,
            };

            await _repository.AddAsync(entity, cancellationToken);

            return entity.Id;
        }
    }
}
