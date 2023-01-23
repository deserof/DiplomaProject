using Manufacturing.Api.Models;
using Manufacturing.Domain.RequestModels;
using Manufacturing.Domain.ResponseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Manufacturing.Api.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int page, int pageStep, int pageSize, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(User.GetClaim(Claims.Subject));
            if (user is null)
            {
                return Challenge(
                    authenticationSchemes: OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                        [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                            "The specified access token is bound to an account that no longer exists."
                    }));
            }

            return Ok(new List<DetailResponse>()
            {
                new DetailResponse { Name = "detaile name" },
                new DetailResponse { Name = "detaile name 2" }
            });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(User.GetClaim(Claims.Subject));
            if (user is null)
            {
                return Challenge(
                    authenticationSchemes: OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                        [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                            "The specified access token is bound to an account that no longer exists."
                    }));
            }

            return Ok(new DetailResponse
            {
                Name = "name by id"
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateDetailRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateDetailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateDetailRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateDetailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
