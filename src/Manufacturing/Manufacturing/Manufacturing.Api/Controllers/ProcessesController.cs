using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Processes.Queries.GetProcessesByProductId;
using Manufacturing.Application.Products.Commands.CreateProduct;
using Manufacturing.Application.Products.Commands.DeleteProduct;
using Manufacturing.Application.Products.Commands.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manufacturing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcessesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<PaginatedList<ProductDto>> GetProcesses([FromQuery] GetProductsWithPaginationQuery query)
        //{
        //    return await _mediator.Send(query);
        //}

        [HttpGet]
        public async Task<PaginatedList<ProcessVm>> GetProcesses([FromQuery] GetProcessesByProductIdQuery query)
        {
            return await _mediator.Send(query);
        }

        //[HttpPost]
        //public async Task<ActionResult<int>> CreateProcess(CreateProductCommand command)
        //{
        //    return await _mediator.Send(command);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProcess(int id, UpdateProductCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _mediator.Send(command);
        //    return Ok();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProcess(int id)
        //{
        //    await _mediator.Send(new DeleteProductCommand(id));
        //    return NoContent();
        //}
    }
}
