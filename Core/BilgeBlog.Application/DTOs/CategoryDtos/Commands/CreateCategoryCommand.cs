using MediatR;

namespace BilgeBlog.Application.DTOs.CategoryDtos.Commands
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}

