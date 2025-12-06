using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetPostLikesQuery : IRequest<PagedResult<PostLikeListItemResult>>
    {
        public Guid PostId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

