using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetPostCommentsQuery : IRequest<PagedResult<CommentListItemResult>>
    {
        public Guid PostId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

