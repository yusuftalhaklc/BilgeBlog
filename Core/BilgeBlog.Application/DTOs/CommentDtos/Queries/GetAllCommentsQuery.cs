using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.CommentDtos.Queries
{
    public class GetAllCommentsQuery : IRequest<PagedResult<CommentResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

