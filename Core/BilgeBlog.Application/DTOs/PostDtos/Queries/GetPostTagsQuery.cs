using BilgeBlog.Application.DTOs.TagDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetPostTagsQuery : IRequest<List<TagResult>>
    {
        public Guid PostId { get; set; }
    }
}

