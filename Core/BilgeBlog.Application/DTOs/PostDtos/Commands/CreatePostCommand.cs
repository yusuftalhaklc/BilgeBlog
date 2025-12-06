using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Commands
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = true;
        public Guid UserId { get; set; }
        public Guid? CategoryId { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}

