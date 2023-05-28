using AutoMapper;
using AutoMapper.QueryableExtensions;
using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Common.Mappings;
using Manufacturing.Application.Common.Models;
using MediatR;

namespace Manufacturing.Application.Details.Queries.GetDetailsWithPagination
{
    public record GetDetailsWithPaginationQuery : IRequest<PaginatedList<DetailDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetDetailsWithPaginationQueryHandler : IRequestHandler<GetDetailsWithPaginationQuery, PaginatedList<DetailDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDetailsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DetailDto>> Handle(GetDetailsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Details
                .ProjectTo<DetailDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
