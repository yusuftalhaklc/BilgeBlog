using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetAllPostsQuery : IRequest<PagedResult<PostResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

