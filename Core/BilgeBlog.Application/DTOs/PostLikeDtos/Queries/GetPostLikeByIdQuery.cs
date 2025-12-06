using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostLikeDtos.Queries
{
    public class GetPostLikeByIdQuery : IRequest<PostLikeResult>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}

