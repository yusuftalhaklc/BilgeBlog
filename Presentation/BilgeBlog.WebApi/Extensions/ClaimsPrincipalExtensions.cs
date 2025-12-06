using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BilgeBlog.WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value 
                ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Fallback for backward compatibility
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User ID not found in token");
            return userId;
        }
    }
}

