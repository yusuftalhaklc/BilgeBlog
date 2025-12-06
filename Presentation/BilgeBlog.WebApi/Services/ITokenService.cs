using BilgeBlog.Application.DTOs.UserDtos.Results;

namespace BilgeBlog.WebApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserResult user);
    }
}

