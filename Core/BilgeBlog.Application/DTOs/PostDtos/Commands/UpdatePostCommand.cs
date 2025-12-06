using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Commands
{
    public class UpdatePostCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public Guid UserId { get; set; }
    }
}

