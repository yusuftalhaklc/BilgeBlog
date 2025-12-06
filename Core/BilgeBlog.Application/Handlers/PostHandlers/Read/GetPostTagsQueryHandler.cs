using AutoMapper;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Application.DTOs.TagDtos.Results;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetPostTagsQueryHandler : IRequestHandler<GetPostTagsQuery, List<TagResult>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostTagsQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<List<TagResult>> Handle(GetPostTagsQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetAll(false)
                .Include(x => x.PostTags)
                    .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);

            if (post == null)
                return new List<TagResult>();

            var tags = post.PostTags?
                .Where(pt => pt.Tag != null)
                .Select(pt => pt.Tag!)
                .ToList() ?? new List<Tag>();

            return _mapper.Map<List<TagResult>>(tags);
        }
    }
}

