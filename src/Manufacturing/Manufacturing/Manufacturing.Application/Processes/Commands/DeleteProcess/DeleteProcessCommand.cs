using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Processes.Commands.DeleteProcess
{
    public record DeleteProcessCommand(int Id) : IRequest;

    public class DeleteProcessCommandHandler : IRequestHandler<DeleteProcessCommand>
    {
        private readonly IGenericRepository<ProcessExecution> _repository;

        public DeleteProcessCommandHandler(IApplicationDbContext context, IGenericRepository<ProcessExecution> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProcessCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
