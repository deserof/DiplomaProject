using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Common.Models;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.ProductionProcesses.Queries.GetProductionProcessesWithPagination
{
    public record GetProductionProcessesWithPaginationQuery : IRequest<PaginatedList<ProductionProcessVm>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetProcessesByProductIdQueryHandler : IRequestHandler<GetProductionProcessesWithPaginationQuery, PaginatedList<ProductionProcessVm>>
    {
        private readonly IGenericRepository<ProductionProcess> _repository;

        public GetProcessesByProductIdQueryHandler(IGenericRepository<ProductionProcess> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<ProductionProcessVm>> Handle(GetProductionProcessesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync<ProductionProcess, ProductionProcessVm>(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
