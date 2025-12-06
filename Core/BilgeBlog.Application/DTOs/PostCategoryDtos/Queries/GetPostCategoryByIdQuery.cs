using BilgeBlog.Application.DTOs.PostCategoryDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostCategoryDtos.Queries
{
    public class GetPostCategoryByIdQuery : IRequest<PostCategoryResult>
    {
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

