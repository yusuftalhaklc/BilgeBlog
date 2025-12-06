using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.RoleDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.RoleDtos.Queries
{
    public class GetAllRolesQuery : IRequest<PagedResult<RoleResult>>
    {
        public Guid CurrentUserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

