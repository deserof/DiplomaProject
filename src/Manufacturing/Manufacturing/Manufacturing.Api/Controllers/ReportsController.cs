using Manufacturing.Application.Reports.Commands.GenerateReport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Manufacturing.Api.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> Generate(
            [FromQuery] DateTime from, 
            [FromQuery] DateTime end)
        {
            var generateReportDto = await _mediator.Send(new GenerateReportCommand
            {
                From = from,
                End = end
            });

            return File(
                generateReportDto.Stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                generateReportDto.ExcelName);
        }
    }
}
