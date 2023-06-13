using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Domain.Entities;
using MediatR;

namespace Manufacturing.Application.Processes.Commands.UploadProcessFile
{
    public record UploadProcessFileCommand : IRequest
    {
        public int Id { get; set; }
        public byte[] ProcessFile { get; set; }
        public string FileName { get; set; }
    }

    public class UploadProcessFileCommandHandler : IRequestHandler<UploadProcessFileCommand>
    {
        private readonly IGenericRepository<ProcessExecution> _processRepository;
        private readonly IGenericRepository<ProductFile> _fileRepository;
        private readonly IApplicationDbContext _context;

        public UploadProcessFileCommandHandler(
            IGenericRepository<ProcessExecution> processRepository,
            IGenericRepository<ProductFile> fileRepository,
            IApplicationDbContext context)
        {
            _processRepository = processRepository;
            _fileRepository = fileRepository;
            _context = context;
        }

        public async Task Handle(UploadProcessFileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _processRepository.GetByIdAsync(request.Id, cancellationToken);

            if (request.ProcessFile != null)
            {
                var file = new ProductFile
                {
                    FileName = request.FileName,
                    FileContent = request.ProcessFile,
                    FileType = FileType.Drawing,
                    ProcessExecutionId = request.Id,
                    ProductId = entity.ProductId
                };
                await _fileRepository.AddAsync(file, cancellationToken);
            }

            var fileId = _context.ProductFiles
                    .First(f => f.FileName == request.FileName && f.ProcessExecutionId == request.Id);

            entity.Id = request.Id;
            entity.ProcessFileId = fileId.Id;

            await _processRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
