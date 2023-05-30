using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string QualityStatus { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IGenericRepository<Product> _repository;

        public CreateProductCommandHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Name = request.Name,
                Description = request.Description,
                QualityStatus = request.QualityStatus,
                CreationDate = DateTime.Now
            };

            await _repository.AddAsync(entity, cancellationToken);

            return entity.Id;
        }
    }
}
