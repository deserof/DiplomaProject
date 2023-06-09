using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Users.Queries.ViewModels;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Users.Queries.GetUsersWithPaginationQuery
{
    public record GetUsersWithPaginationQuery : IRequest<PaginatedList<UserVm>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetProductsQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserVm>>
    {
        private readonly IGenericRepository<Employee> _repository;

        public GetProductsQueryHandler(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<UserVm>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync<Employee, UserVm>(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
