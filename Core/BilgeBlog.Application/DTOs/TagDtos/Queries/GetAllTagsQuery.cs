using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.TagDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.TagDtos.Queries
{
    public class GetAllTagsQuery : IRequest<PagedResult<TagResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

