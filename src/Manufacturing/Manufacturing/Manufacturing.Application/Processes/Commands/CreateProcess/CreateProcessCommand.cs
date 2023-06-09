using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Processes.Commands.CreateProcess
{
    public record CreateProcessCommand : IRequest<int>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EmployeeId { get; set; }
        public int ProcessId { get; set; }
        public int ProductId { get; set; }
    }

    public class CreateProcessCommandHandler : IRequestHandler<CreateProcessCommand, int>
    {
        private readonly IGenericRepository<ProcessExecution> _repository;

        public CreateProcessCommandHandler(IGenericRepository<ProcessExecution> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProcessCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProcessExecution()
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                EmployeeId = request.EmployeeId,
                ProductId = request.ProductId,
                ProcessId = request.ProcessId,
            };

            await _repository.AddAsync(entity, cancellationToken);

            return entity.Id;
        }
    }
}
