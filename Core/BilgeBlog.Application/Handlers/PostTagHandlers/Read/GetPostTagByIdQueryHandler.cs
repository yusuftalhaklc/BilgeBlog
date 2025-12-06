using AutoMapper;
using BilgeBlog.Application.DTOs.PostTagDtos.Queries;
using BilgeBlog.Application.DTOs.PostTagDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostTagHandlers.Read
{
    public class GetPostTagByIdQueryHandler : IRequestHandler<GetPostTagByIdQuery, PostTagResult>
    {
        private readonly IPostTagRepository _postTagRepository;
        private readonly IMapper _mapper;

        public GetPostTagByIdQueryHandler(IPostTagRepository postTagRepository, IMapper mapper)
        {
            _postTagRepository = postTagRepository;
            _mapper = mapper;
        }

        public async Task<PostTagResult> Handle(GetPostTagByIdQuery request, CancellationToken cancellationToken)
        {
            var postTag = await _postTagRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.Tag)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.TagId == request.TagId, cancellationToken);

            if (postTag == null)
                return null!;

            return _mapper.Map<PostTagResult>(postTag);
        }
    }
}

