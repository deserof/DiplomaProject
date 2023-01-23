using Manufacturing.Api.Models;
using Manufacturing.Domain.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Manufacturing.Api.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public QrCodeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        //[ProducesResponseType(typeof(???), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateAsync(int DetailId, CancellationToken cancellationToken)
        {
            return Ok(new List<DetailResponse>()
            {
                new DetailResponse { Name = "detaile name" },
                new DetailResponse { Name = "detaile name 2" }
            });
        }
    }
}
