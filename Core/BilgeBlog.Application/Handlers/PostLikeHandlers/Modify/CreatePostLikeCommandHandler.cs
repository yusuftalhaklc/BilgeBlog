using BilgeBlog.Application.DTOs.PostLikeDtos.Commands;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostLikeHandlers.Modify
{
    public class CreatePostLikeCommandHandler : IRequestHandler<CreatePostLikeCommand, bool>
    {
        private readonly IPostLikeRepository _postLikeRepository;

        public CreatePostLikeCommandHandler(IPostLikeRepository postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }

        public async Task<bool> Handle(CreatePostLikeCommand request, CancellationToken cancellationToken)
        {
            var existing = await _postLikeRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.UserId == request.UserId, cancellationToken);

            if (existing != null)
                return false;

            var postLike = new PostLike
            {
                PostId = request.PostId,
                UserId = request.UserId
            };

            return await _postLikeRepository.AddAsync(postLike);
        }
    }
}

