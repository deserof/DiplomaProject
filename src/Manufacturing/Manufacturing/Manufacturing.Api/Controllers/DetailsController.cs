using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Details.Commands.CreateDetail;
using Manufacturing.Application.Details.Commands.DeleteDetail;
using Manufacturing.Application.Details.Commands.UpdateDetail;
using Manufacturing.Application.Details.Queries.GetDetail;
using Manufacturing.Application.Details.Queries.GetDetailsWithPagination;
using Manufacturing.Application.Details.Queries.ViewModels;
using Manufacturing.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Manufacturing.Api.Controllers
{
    // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<DetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PaginatedList<DetailDto>> Get([FromQuery] GetDetailsWithPaginationQuery query)
        {
            var user = await _userManager.FindByIdAsync(User.GetClaim(Claims.Subject));
            if (user is null)
            {
                //return Challenge(
                //    authenticationSchemes: OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                //    properties: new AuthenticationProperties(new Dictionary<string, string>
                //    {
                //        [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                //        [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                //            "The specified access token is bound to an account that no longer exists."
                //    }));
            }

            return await _mediator.Send(query);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DetailDto>> Get(int id)
        {
            return await _mediator.Send(new GetDetailQuery(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateDetailCommand), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> Create(CreateDetailCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateDetailCommand), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateDetailCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteDetailsCommand), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteDetailsCommand(id));

            return NoContent();
        }
    }
}
