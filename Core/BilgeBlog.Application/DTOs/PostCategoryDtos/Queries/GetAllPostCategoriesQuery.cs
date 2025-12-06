using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostCategoryDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.PostCategoryDtos.Queries
{
    public class GetAllPostCategoriesQuery : IRequest<PagedResult<PostCategoryResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

