using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostLikeDtos.Queries
{
    public class GetAllPostLikesQuery : IRequest<PagedResult<PostLikeResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

