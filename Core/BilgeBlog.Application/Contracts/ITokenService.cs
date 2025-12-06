using BilgeBlog.Application.DTOs.UserDtos.Results;
using System.Security.Claims;

namespace BilgeBlog.Application.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(UserResult user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}

