using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetPostLikesQuery : IRequest<List<PostLikeResult>>
    {
        public Guid PostId { get; set; }
    }
}

