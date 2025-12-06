using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Commands
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

