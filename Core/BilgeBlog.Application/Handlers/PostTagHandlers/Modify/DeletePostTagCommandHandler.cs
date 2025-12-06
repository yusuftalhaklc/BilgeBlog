using BilgeBlog.Application.DTOs.PostTagDtos.Commands;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostTagHandlers.Modify
{
    public class DeletePostTagCommandHandler : IRequestHandler<DeletePostTagCommand, bool>
    {
        private readonly IPostTagRepository _postTagRepository;

        public DeletePostTagCommandHandler(IPostTagRepository postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }

        public async Task<bool> Handle(DeletePostTagCommand request, CancellationToken cancellationToken)
        {
            var postTag = await _postTagRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.TagId == request.TagId, cancellationToken);

            if (postTag == null)
                return false;

            return await _postTagRepository.Remove(postTag);
        }
    }
}

