using BilgeBlog.Application.DTOs.UserDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Commands
{
    public class LoginUserCommand : IRequest<LoginResult>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

