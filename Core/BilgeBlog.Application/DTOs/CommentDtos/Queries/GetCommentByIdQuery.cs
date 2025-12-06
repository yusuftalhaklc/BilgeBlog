using BilgeBlog.Application.DTOs.CommentDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.CommentDtos.Queries
{
    public class GetCommentByIdQuery : IRequest<CommentResult>
    {
        public Guid Id { get; set; }
    }
}

