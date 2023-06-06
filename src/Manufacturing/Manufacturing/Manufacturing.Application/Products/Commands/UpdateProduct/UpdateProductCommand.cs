using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string QualityStatus { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IGenericRepository<Product> _repository;

        public UpdateProductCommandHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            entity.Id = request.Id;
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.QualityStatus = request.QualityStatus;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
