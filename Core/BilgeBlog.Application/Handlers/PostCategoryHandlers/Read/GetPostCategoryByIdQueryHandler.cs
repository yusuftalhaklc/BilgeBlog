using AutoMapper;
using BilgeBlog.Application.DTOs.PostCategoryDtos.Queries;
using BilgeBlog.Application.DTOs.PostCategoryDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostCategoryHandlers.Read
{
    public class GetPostCategoryByIdQueryHandler : IRequestHandler<GetPostCategoryByIdQuery, PostCategoryResult>
    {
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IMapper _mapper;

        public GetPostCategoryByIdQueryHandler(IPostCategoryRepository postCategoryRepository, IMapper mapper)
        {
            _postCategoryRepository = postCategoryRepository;
            _mapper = mapper;
        }

        public async Task<PostCategoryResult> Handle(GetPostCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var postCategory = await _postCategoryRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.CategoryId == request.CategoryId, cancellationToken);

            if (postCategory == null)
                return null!;

            return _mapper.Map<PostCategoryResult>(postCategory);
        }
    }
}

