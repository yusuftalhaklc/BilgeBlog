using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.UserDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Queries
{
    public class GetAllUsersQuery : IRequest<PagedResult<UserResult>>
    {
        public Guid CurrentUserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

