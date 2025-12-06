using BilgeBlog.Application.DTOs.CommentDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetPostCommentsQuery : IRequest<List<CommentResult>>
    {
        public Guid PostId { get; set; }
    }
}

