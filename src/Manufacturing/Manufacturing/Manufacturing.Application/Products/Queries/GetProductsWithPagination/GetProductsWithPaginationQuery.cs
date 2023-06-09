using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Products.Queries.ViewModels;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Products.Queries.GetProductsWithPagination
{
    public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductVm>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductVm>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetProductsQueryHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<ProductVm>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync<Product, ProductVm>(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
