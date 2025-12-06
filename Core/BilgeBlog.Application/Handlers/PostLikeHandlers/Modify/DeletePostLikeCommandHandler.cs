using BilgeBlog.Application.DTOs.PostLikeDtos.Commands;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostLikeHandlers.Modify
{
    public class DeletePostLikeCommandHandler : IRequestHandler<DeletePostLikeCommand, bool>
    {
        private readonly IPostLikeRepository _postLikeRepository;

        public DeletePostLikeCommandHandler(IPostLikeRepository postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }

        public async Task<bool> Handle(DeletePostLikeCommand request, CancellationToken cancellationToken)
        {
            var postLike = await _postLikeRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.UserId == request.UserId, cancellationToken);

            if (postLike == null)
                return false;

            return await _postLikeRepository.Remove(postLike);
        }
    }
}

