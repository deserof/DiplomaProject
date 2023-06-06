using AutoMapper;
using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manufacturing.Application.Processes.Queries.GetProcessesByProductId
{
    public record GetProcessesByProductIdQuery : IRequest<PaginatedList<ProcessVm>>
    {
        public int ProductId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetProcessesByProductIdQueryHandler : IRequestHandler<GetProcessesByProductIdQuery, PaginatedList<ProcessVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProcessesByProductIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProcessVm>> Handle(GetProcessesByProductIdQuery request,
            CancellationToken cancellationToken)
        {
            var processes = _context.ProcessExecutions
                .Where(x => x.ProductId == request.ProductId)
                .Include(x => x.ProductionProcess)
                .Select(x => new ProcessVm()
                {
                    ProcessExecutionId = x.Id,
                    Name = x.ProductionProcess.Name,
                    Description = x.ProductionProcess.Description,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    ProductionProcessId = x.ProductionProcess.Id,
                });

            return await PaginatedList<ProcessVm>.CreateAsync(processes, request.PageNumber, request.PageSize);
        }
    }
}
