using MediatR;

namespace BilgeBlog.Application.DTOs.CommentDtos.Commands
{
    public class DeleteCommentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

