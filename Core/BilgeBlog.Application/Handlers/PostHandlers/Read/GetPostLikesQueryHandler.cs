using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetPostLikesQueryHandler : IRequestHandler<GetPostLikesQuery, PagedResult<PostLikeListItemResult>>
    {
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;

        public GetPostLikesQueryHandler(IPostLikeRepository postLikeRepository, IMapper mapper)
        {
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostLikeListItemResult>> Handle(GetPostLikesQuery request, CancellationToken cancellationToken)
        {
            var query = _postLikeRepository.GetAll(false)
                .Include(x => x.User)
                .Where(x => x.PostId == request.PostId);

            var totalCount = await query.CountAsync(cancellationToken);

            var likes = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<PostLikeListItemResult>
            {
                Data = _mapper.Map<List<PostLikeListItemResult>>(likes),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

