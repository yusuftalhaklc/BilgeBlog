using MediatR;

namespace BilgeBlog.Application.DTOs.RoleDtos.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}

