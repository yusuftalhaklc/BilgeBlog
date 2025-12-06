using MediatR;

namespace BilgeBlog.Application.DTOs.TagDtos.Commands
{
    public class DeleteTagCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

