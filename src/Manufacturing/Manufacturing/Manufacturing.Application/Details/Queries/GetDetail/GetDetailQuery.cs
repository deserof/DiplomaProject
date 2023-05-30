using AutoMapper;
using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Details.Queries.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manufacturing.Application.Details.Queries.GetDetail
{
    public record GetDetailQuery(int Id) : IRequest<DetailDto>;

    public class GetDetailQueryHandler : IRequestHandler<GetDetailQuery, DetailDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetailDto> Handle(GetDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Details
                .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

            return _mapper.Map<DetailDto>(entity);
        }
    }
}
