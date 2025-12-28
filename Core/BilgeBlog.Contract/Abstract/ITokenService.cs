using BilgeBlog.Domain.Entities;
using System.Security.Claims;

namespace BilgeBlog.Contract.Abstract
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, Role userRole);
        string GenerateRefreshToken();
        Guid GetUserIdFromToken(string token);
    }
}
