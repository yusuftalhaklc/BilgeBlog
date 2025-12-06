using MediatR;

namespace BilgeBlog.Application.DTOs.TagDtos.Commands
{
    public class CreateTagCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}

