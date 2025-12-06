using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetAllPostsQuery : IRequest<PagedResult<PostListItemResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Guid? UserId { get; set; }
        public string? Search { get; set; }
        public string? TagName { get; set; }
        public Guid? CategoryId { get; set; }
    }
}

