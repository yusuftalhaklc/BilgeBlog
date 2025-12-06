using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}

