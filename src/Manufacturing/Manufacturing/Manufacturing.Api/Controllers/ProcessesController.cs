using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Processes.Commands.CreateProcess;
using Manufacturing.Application.Processes.Commands.DeleteProcess;
using Manufacturing.Application.Processes.Commands.UploadProcessFile;
using Manufacturing.Application.Processes.Commands.UploadProcessPhoto;
using Manufacturing.Application.Processes.Queries.GetProcessesByProductId;
using Manufacturing.Application.Processes.Queries.GetProcessPhoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Manufacturing.Api.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcessesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PaginatedList<ProcessVm>> GetProcesses([FromQuery] GetProcessesByProductIdQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProcess(CreateProcessCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}/photo")]
        public async Task<IActionResult> UploadProcessPhoto(int id, IFormFile processPhoto)
        {
            byte[] processPhotoBytes = null;

            if (processPhoto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await processPhoto.CopyToAsync(memoryStream);
                    processPhotoBytes = memoryStream.ToArray();
                }
            }

            var command = new UploadProcessPhotoCommand()
            {
                Id = id,
                ProcessPhoto = processPhotoBytes,
                FileName = processPhoto?.FileName,
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/file")]
        public async Task<IActionResult> UploadProcessFile(int id, IFormFile processFile)
        {
            byte[] processFileBytes = null;

            if (processFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await processFile.CopyToAsync(memoryStream);
                    processFileBytes = memoryStream.ToArray();
                }
            }

            var command = new UploadProcessFileCommand()
            {
                Id = id,
                ProcessFile = processFileBytes,
                FileName = processFile?.FileName,
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(int id)
        {
            await _mediator.Send(new DeleteProcessCommand(id));
            return NoContent();
        }

        [HttpGet("{id}/photos")]
        public async Task<ActionResult<List<ProcessPhotoVm>>> GetPhotos(int id)
        {
            return await _mediator.Send(new GetProcessPhotoQuery(id));
        }
    }
}
