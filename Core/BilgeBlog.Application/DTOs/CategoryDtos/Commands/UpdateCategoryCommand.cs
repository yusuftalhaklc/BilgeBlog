using MediatR;

namespace BilgeBlog.Application.DTOs.CategoryDtos.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

