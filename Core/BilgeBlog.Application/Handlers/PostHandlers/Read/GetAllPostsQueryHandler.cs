using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedResult<PostResult>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IPostRepository postRepository, IPostLikeRepository postLikeRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostResult>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _postRepository.GetAll(false)
                .Include(x => x.User)
                .Include(x => x.PostTags)
                    .ThenInclude(x => x.Tag)
                .Include(x => x.PostCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.Likes)
                .Include(x => x.Comments);

            var totalCount = await query.CountAsync(cancellationToken);

            var posts = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var postResults = _mapper.Map<List<PostResult>>(posts);

            if (request.UserId.HasValue && postResults.Any())
            {
                var postIds = postResults.Select(p => p.Id).ToList();
                var likedPostIds = await _postLikeRepository.GetAll(false)
                    .Where(x => postIds.Contains(x.PostId) && x.UserId == request.UserId.Value)
                    .Select(x => x.PostId)
                    .ToListAsync(cancellationToken);

                foreach (var postResult in postResults)
                {
                    postResult.IsLiked = likedPostIds.Contains(postResult.Id);
                }
            }

            return new PagedResult<PostResult>
            {
                Data = postResults,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

