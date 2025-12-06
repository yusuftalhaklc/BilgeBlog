using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedResult<PostListItemResult>>
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

        public async Task<PagedResult<PostListItemResult>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _postRepository.GetAll(false);

            // Search: Title veya Content'te arama
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                baseQuery = baseQuery.Where(x => x.Title.Contains(request.Search) || x.Content.Contains(request.Search));
            }

            // TagName: Tag ismine göre arama
            if (!string.IsNullOrWhiteSpace(request.TagName))
            {
                baseQuery = baseQuery.Where(x => x.PostTags.Any(pt => pt.Tag != null && pt.Tag.Name.Contains(request.TagName)));
            }

            // CategoryId: Category ID'ye göre filtreleme
            if (request.CategoryId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.PostCategories.Any(pc => pc.CategoryId == request.CategoryId.Value));
            }

            var query = baseQuery
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

            var postResults = _mapper.Map<List<PostListItemResult>>(posts);

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

            return new PagedResult<PostListItemResult>
            {
                Data = postResults,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

