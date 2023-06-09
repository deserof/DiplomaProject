using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Products.Queries.ViewModels;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Products.Queries.GetProductById
{
    public record GetProductQuery(int Id) : IRequest<ProductVm>;

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductVm>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetProductQueryHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductVm> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            var productVm = new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
            };

            return productVm;
        }
    }
}
