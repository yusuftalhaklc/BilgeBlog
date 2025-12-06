using MediatR;

namespace BilgeBlog.Application.DTOs.TagDtos.Commands
{
    public class UpdateTagCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

