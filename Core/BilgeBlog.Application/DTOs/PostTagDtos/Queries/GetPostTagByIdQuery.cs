using BilgeBlog.Application.DTOs.PostTagDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostTagDtos.Queries
{
    public class GetPostTagByIdQuery : IRequest<PostTagResult>
    {
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }
    }
}

