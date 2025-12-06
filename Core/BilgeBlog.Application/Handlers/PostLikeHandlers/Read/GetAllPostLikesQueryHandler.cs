using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostLikeDtos.Queries;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostLikeHandlers.Read
{
    public class GetAllPostLikesQueryHandler : IRequestHandler<GetAllPostLikesQuery, PagedResult<PostLikeResult>>
    {
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;

        public GetAllPostLikesQueryHandler(IPostLikeRepository postLikeRepository, IMapper mapper)
        {
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostLikeResult>> Handle(GetAllPostLikesQuery request, CancellationToken cancellationToken)
        {
            var query = _postLikeRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.User);

            var totalCount = await query.CountAsync(cancellationToken);

            var postLikes = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<PostLikeResult>
            {
                Data = _mapper.Map<List<PostLikeResult>>(postLikes),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

