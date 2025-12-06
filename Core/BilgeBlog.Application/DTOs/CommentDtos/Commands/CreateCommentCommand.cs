using MediatR;

namespace BilgeBlog.Application.DTOs.CommentDtos.Commands
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public string Message { get; set; } = string.Empty;
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentCommentId { get; set; }
    }
}

