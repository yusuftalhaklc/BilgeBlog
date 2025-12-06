using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostTagDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostTagDtos.Queries
{
    public class GetAllPostTagsQuery : IRequest<PagedResult<PostTagResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

