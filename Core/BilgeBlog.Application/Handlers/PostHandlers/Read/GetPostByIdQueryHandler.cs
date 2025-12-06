using AutoMapper;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostResult>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IPostRepository postRepository, IPostLikeRepository postLikeRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
        }

        public async Task<PostResult> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetAll(false)
                .Include(x => x.User)
                .Include(x => x.PostTags)
                    .ThenInclude(x => x.Tag)
                .Include(x => x.PostCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Likes)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (post == null)
                throw new NotFoundException("Post", request.Id);

            var postResult = _mapper.Map<PostResult>(post);

            if (request.UserId.HasValue)
            {
                var isLiked = await _postLikeRepository.GetAll(false)
                    .AnyAsync(x => x.PostId == postResult.Id && x.UserId == request.UserId.Value, cancellationToken);
                postResult.IsLiked = isLiked;
            }

            return postResult;
        }
    }
}

