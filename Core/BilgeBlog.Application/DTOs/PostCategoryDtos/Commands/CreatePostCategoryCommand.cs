using MediatR;

namespace BilgeBlog.Application.DTOs.PostCategoryDtos.Commands
{
    public class CreatePostCategoryCommand : IRequest<bool>
    {
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

