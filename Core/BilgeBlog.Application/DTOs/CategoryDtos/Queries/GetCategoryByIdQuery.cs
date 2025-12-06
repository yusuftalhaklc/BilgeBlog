using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.CategoryDtos.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryResult>
    {
        public Guid Id { get; set; }
    }
}

