using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Commands
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

