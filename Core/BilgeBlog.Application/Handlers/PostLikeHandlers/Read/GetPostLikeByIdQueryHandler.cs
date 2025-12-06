using AutoMapper;
using BilgeBlog.Application.DTOs.PostLikeDtos.Queries;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostLikeHandlers.Read
{
    public class GetPostLikeByIdQueryHandler : IRequestHandler<GetPostLikeByIdQuery, PostLikeResult>
    {
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;

        public GetPostLikeByIdQueryHandler(IPostLikeRepository postLikeRepository, IMapper mapper)
        {
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
        }

        public async Task<PostLikeResult> Handle(GetPostLikeByIdQuery request, CancellationToken cancellationToken)
        {
            var postLike = await _postLikeRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.UserId == request.UserId, cancellationToken);

            if (postLike == null)
                return null!;

            return _mapper.Map<PostLikeResult>(postLike);
        }
    }
}

