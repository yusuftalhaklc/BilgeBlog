using MediatR;

namespace BilgeBlog.Application.DTOs.RoleDtos.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}

