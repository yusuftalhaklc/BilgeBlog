using AutoMapper;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetPostLikesQueryHandler : IRequestHandler<GetPostLikesQuery, List<PostLikeResult>>
    {
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;

        public GetPostLikesQueryHandler(IPostLikeRepository postLikeRepository, IMapper mapper)
        {
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
        }

        public async Task<List<PostLikeResult>> Handle(GetPostLikesQuery request, CancellationToken cancellationToken)
        {
            var likes = await _postLikeRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.User)
                .Where(x => x.PostId == request.PostId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<PostLikeResult>>(likes);
        }
    }
}

