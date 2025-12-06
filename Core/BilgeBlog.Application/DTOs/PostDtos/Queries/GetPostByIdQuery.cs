using BilgeBlog.Application.DTOs.PostDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostDtos.Queries
{
    public class GetPostByIdQuery : IRequest<PostResult>
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
    }
}

