using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int Id) : IRequest;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IGenericRepository<Product> _repository;

        public DeleteProductCommandHandler(IApplicationDbContext context, IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
