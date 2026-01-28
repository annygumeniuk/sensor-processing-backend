using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SensorProcessing.BusinessLogic.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace SensorProcessing.BusinessLogic.Services.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal? _user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor.HttpContext?.User;
        }

        public Guid? GetUserId()
        {
            var userId =
                _user?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                _user?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(userId, out var id) ? id : null;
        }

        public string? GetEmail()
        {
            return
                _user?.FindFirstValue(ClaimTypes.Email) ??
                _user?.FindFirstValue(JwtRegisteredClaimNames.Email);
        }

        public bool IsAuthenticated()
        {
            return _user?.Identity?.IsAuthenticated ?? false;
        }
    }
}
