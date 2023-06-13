using Manufacturing.Application.Common.Models;
using Manufacturing.Application.Users.Queries.GetUsersWithPaginationQuery;
using Manufacturing.Application.Users.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Manufacturing.Api.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<PaginatedList<UserVm>> GetUsers([FromQuery] GetUsersWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
