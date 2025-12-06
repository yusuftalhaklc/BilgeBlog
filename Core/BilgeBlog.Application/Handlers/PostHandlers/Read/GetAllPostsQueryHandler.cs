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
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
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

            return new PagedResult<PostResult>
            {
                Data = _mapper.Map<List<PostResult>>(posts),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

