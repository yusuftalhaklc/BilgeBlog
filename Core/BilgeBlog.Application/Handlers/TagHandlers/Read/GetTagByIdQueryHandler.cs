using AutoMapper;
using BilgeBlog.Application.DTOs.TagDtos.Queries;
using BilgeBlog.Application.DTOs.TagDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;

namespace BilgeBlog.Application.Handlers.TagHandlers.Read
{
    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagResult>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public GetTagByIdQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<TagResult> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(request.Id, false);
            if (tag == null)
                return null!;

            return _mapper.Map<TagResult>(tag);
        }
    }
}

