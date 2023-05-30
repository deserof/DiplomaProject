using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Products.Commands.CreateProduct;
using Manufacturing.Application.Products.Queries.GetProductsWithPagination;
using Manufacturing.Application.Products.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manufacturing.Api.Controllers
{
    // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PaginatedList<ProductDto>> GetProducts([FromQuery] GetProductsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProduct(int id)
        //{
        //    var product = await _mediator.Send(new GetProductQuery(id));
        //    return Ok(product);
        //}

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var product = await _mediator.Send(command);
        //    return Ok(product);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var product = await _mediator.Send(new DeleteProductCommand(id));
        //    return Ok(product);
        //}
    }
}
