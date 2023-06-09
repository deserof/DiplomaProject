using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Processes.Commands.UploadProcessPhoto
{
    public record UploadProcessPhotoCommand : IRequest
    {
        public int Id { get; set; }
        public byte[] ProcessPhoto { get; set; }
        public string FileName { get; set; }
    }

    public class UploadProcessPhotoCommandHandler : IRequestHandler<UploadProcessPhotoCommand>
    {
        private readonly IGenericRepository<ProcessExecution> _processRepository;
        private readonly IGenericRepository<ProductFile> _fileRepository;
        private readonly IApplicationDbContext _context;

        public UploadProcessPhotoCommandHandler(
            IGenericRepository<ProcessExecution> processRepository,
            IGenericRepository<ProductFile> fileRepository,
            IApplicationDbContext context)
        {
            _processRepository = processRepository;
            _fileRepository = fileRepository;
            _context = context;
        }

        public async Task Handle(UploadProcessPhotoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _processRepository.GetByIdAsync(request.Id, cancellationToken);

            if (request.ProcessPhoto != null)
            {
                var file = new ProductFile
                {
                    FileName = request.FileName,
                    FileContent = request.ProcessPhoto,
                    FileType = FileType.Photo,
                    ProcessExecutionId = request.Id,
                    ProductId = entity.ProductId
                };
                await _fileRepository.AddAsync(file, cancellationToken);
            }

            var fileId = _context.ProductFiles
                    .First(f => f.FileName == request.FileName && f.ProcessExecutionId == request.Id);

            entity.Id = request.Id;
            entity.ProcessPhotoId = fileId.Id;

            await _processRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
