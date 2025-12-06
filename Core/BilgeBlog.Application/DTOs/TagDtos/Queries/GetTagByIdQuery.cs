using BilgeBlog.Application.DTOs.TagDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.TagDtos.Queries
{
    public class GetTagByIdQuery : IRequest<TagResult>
    {
        public Guid Id { get; set; }
    }
}

