using System.Security.Claims;
using Manufacturing.Application.Common.Interfaces;
using OpenIddict.Abstractions;

namespace Manufacturing.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIddictConstants.Claims.Name);
    }
}
