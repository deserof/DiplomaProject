using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manufacturing.Application.Processes.Queries.GetProcessPhoto
{
    public record GetProcessPhotoQuery(int Id) : IRequest<List<ProcessPhotoVm>>;

    public class GetProcessPhotoQueryHandler : IRequestHandler<GetProcessPhotoQuery, List<ProcessPhotoVm>>
    {
        private readonly IApplicationDbContext _context;

        public GetProcessPhotoQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProcessPhotoVm>> Handle(GetProcessPhotoQuery request, CancellationToken cancellationToken)
        {
            return await _context.ProductFiles
                .Where(x => x.ProcessExecutionId == request.Id && x.FileType == FileType.Photo)
                .Select(x => new ProcessPhotoVm
                {
                    Id = x.Id,
                    ImageData = x.FileContent,
                    ProcessId = x.ProcessExecutionId.Value
                })
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
