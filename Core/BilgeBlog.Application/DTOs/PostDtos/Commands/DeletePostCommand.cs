using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Commands
{
    public class DeletePostCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}

