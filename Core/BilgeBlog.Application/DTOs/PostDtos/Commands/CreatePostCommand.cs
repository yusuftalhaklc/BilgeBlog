using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Commands
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; } = true;
        public Guid UserId { get; set; }
    }
}

