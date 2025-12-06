using MediatR;

namespace BilgeBlog.Application.DTOs.CategoryDtos.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

