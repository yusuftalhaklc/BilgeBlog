using MediatR;

namespace BilgeBlog.Application.DTOs.CommentDtos.Commands
{
    public class UpdateCommentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}

