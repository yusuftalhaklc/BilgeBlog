using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.CategoryDtos.Queries
{
    public class GetAllCategoriesQuery : IRequest<PagedResult<CategoryResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

