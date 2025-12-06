using MediatR;

namespace BilgeBlog.Application.DTOs.RoleDtos.Commands
{
    public class CreateRoleCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}

