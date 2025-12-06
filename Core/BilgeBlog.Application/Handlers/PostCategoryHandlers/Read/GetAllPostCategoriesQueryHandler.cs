using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostCategoryDtos.Queries;
using BilgeBlog.Application.DTOs.PostCategoryDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostCategoryHandlers.Read
{
    public class GetAllPostCategoriesQueryHandler : IRequestHandler<GetAllPostCategoriesQuery, PagedResult<PostCategoryResult>>
    {
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IMapper _mapper;

        public GetAllPostCategoriesQueryHandler(IPostCategoryRepository postCategoryRepository, IMapper mapper)
        {
            _postCategoryRepository = postCategoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostCategoryResult>> Handle(GetAllPostCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _postCategoryRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.Category);

            var totalCount = await query.CountAsync(cancellationToken);

            var postCategories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<PostCategoryResult>
            {
                Data = _mapper.Map<List<PostCategoryResult>>(postCategories),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

