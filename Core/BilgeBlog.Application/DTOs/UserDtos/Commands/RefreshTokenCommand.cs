using BilgeBlog.Application.DTOs.UserDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Commands
{
    public class RefreshTokenCommand : IRequest<LoginResult>
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}

