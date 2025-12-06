using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.TagDtos.Queries;
using BilgeBlog.Application.DTOs.TagDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.TagHandlers.Read
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, PagedResult<TagResult>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public GetAllTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<TagResult>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var query = _tagRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var tags = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<TagResult>
            {
                Data = _mapper.Map<List<TagResult>>(tags),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

