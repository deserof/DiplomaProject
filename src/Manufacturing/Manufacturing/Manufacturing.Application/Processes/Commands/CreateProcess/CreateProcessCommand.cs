using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Processes.Commands.CreateProcess
{
    public record CreateProcessCommand : IRequest<int>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ProductionProcessId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
    }

    public class CreateProcessCommandHandler : IRequestHandler<CreateProcessCommand, int>
    {
        private readonly IGenericRepository<ProcessExecution> _repository;
        private readonly ICurrentUserService _currentUserService;

        public CreateProcessCommandHandler(
            IGenericRepository<ProcessExecution> repository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateProcessCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProcessExecution()
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                ProductId = request.ProductId,
                ProcessId = request.ProductionProcessId,
                Description = request.Description,
            };

            await _repository.AddAsync(entity, cancellationToken);

            return entity.Id;
        }
    }
}
