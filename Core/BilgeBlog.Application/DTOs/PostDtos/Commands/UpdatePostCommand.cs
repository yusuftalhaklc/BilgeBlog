using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Commands
{
    public class UpdatePostCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public Guid UserId { get; set; }
    }
}

