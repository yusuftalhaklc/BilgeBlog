using BilgeBlog.Application.DTOs.UserDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Commands
{
    public class LoginUserCommand : IRequest<UserResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

