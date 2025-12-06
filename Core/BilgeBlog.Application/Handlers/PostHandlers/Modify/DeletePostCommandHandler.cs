using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;

namespace BilgeBlog.Application.Handlers.PostHandlers.Modify
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);
            if (post == null)
                throw new NotFoundException("Post", request.Id);

            return await _postRepository.RemoveAsync(request.Id);
        }
    }
}

