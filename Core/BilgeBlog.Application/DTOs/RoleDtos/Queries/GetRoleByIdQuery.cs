using BilgeBlog.Application.DTOs.RoleDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.RoleDtos.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleResult>
    {
        public Guid Id { get; set; }
    }
}

