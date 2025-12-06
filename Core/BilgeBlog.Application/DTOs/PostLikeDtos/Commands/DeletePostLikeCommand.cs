using MediatR;

namespace BilgeBlog.Application.DTOs.PostLikeDtos.Commands
{
    public class DeletePostLikeCommand : IRequest<bool>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}

