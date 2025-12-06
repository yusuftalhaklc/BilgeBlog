using BilgeBlog.Application.DTOs.PostTagDtos.Commands;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostTagHandlers.Modify
{
    public class CreatePostTagCommandHandler : IRequestHandler<CreatePostTagCommand, bool>
    {
        private readonly IPostTagRepository _postTagRepository;

        public CreatePostTagCommandHandler(IPostTagRepository postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }

        public async Task<bool> Handle(CreatePostTagCommand request, CancellationToken cancellationToken)
        {
            var existing = await _postTagRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.TagId == request.TagId, cancellationToken);

            if (existing != null)
                return false;

            var postTag = new PostTag
            {
                PostId = request.PostId,
                TagId = request.TagId
            };

            return await _postTagRepository.AddAsync(postTag);
        }
    }
}

