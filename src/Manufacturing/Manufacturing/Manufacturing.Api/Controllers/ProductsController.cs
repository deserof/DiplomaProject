using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Products.Commands.CreateProduct;
using Manufacturing.Application.Products.Commands.DeleteProduct;
using Manufacturing.Application.Products.Commands.UpdateProduct;
using Manufacturing.Application.Products.Queries.GetProductById;
using Manufacturing.Application.Products.Queries.GetProductsWithPagination;
using Manufacturing.Application.Products.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Manufacturing.Api.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PaginatedList<ProductVm>> GetProducts([FromQuery] GetProductsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _mediator.Send(new GetProductQuery(id));
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
