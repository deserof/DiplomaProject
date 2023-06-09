using Manufacturing.Application.Common.Models;
using Manufacturing.Application.ProductionProcesses.Commands.CreateProductionProcess;
using Manufacturing.Application.ProductionProcesses.Commands.DeleteProductionProcess;
using Manufacturing.Application.ProductionProcesses.Commands.UpdateProductionProcess;
using Manufacturing.Application.ProductionProcesses.Queries.GetProductionProcessesWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Manufacturing.Api.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionProcessesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductionProcessesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PaginatedList<ProductionProcessVm>> GetProductionProcesses([FromQuery] GetProductionProcessesWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProductionProcess(CreateProductionProcessCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductionProcessCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionProcess(int id)
        {
            await _mediator.Send(new DeleteProductionProcessCommand(id));
            return NoContent();
        }
    }
}
