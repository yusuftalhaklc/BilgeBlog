using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostTagDtos.Queries;
using BilgeBlog.Application.DTOs.PostTagDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostTagHandlers.Read
{
    public class GetAllPostTagsQueryHandler : IRequestHandler<GetAllPostTagsQuery, PagedResult<PostTagResult>>
    {
        private readonly IPostTagRepository _postTagRepository;
        private readonly IMapper _mapper;

        public GetAllPostTagsQueryHandler(IPostTagRepository postTagRepository, IMapper mapper)
        {
            _postTagRepository = postTagRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostTagResult>> Handle(GetAllPostTagsQuery request, CancellationToken cancellationToken)
        {
            var query = _postTagRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.Tag);

            var totalCount = await query.CountAsync(cancellationToken);

            var postTags = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<PostTagResult>
            {
                Data = _mapper.Map<List<PostTagResult>>(postTags),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

