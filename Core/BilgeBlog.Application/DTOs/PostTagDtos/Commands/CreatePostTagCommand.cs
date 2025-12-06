using MediatR;

namespace BilgeBlog.Application.DTOs.PostTagDtos.Commands
{
    public class CreatePostTagCommand : IRequest<bool>
    {
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }
    }
}

