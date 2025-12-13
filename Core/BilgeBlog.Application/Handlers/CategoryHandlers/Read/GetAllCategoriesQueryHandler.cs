using AutoMapper;
using BilgeBlog.Application.DTOs.CategoryDtos.Queries;
using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.CategoryHandlers.Read
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryResult>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CategoryResult>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _categoryRepository.GetAll(false);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search));
            }

            query = request.SortBy switch
            {
                Domain.Enums.CategorySortBy.Name => request.Sort == Domain.Enums.SortOrder.Asc
                    ? query.OrderBy(x => x.Name)
                    : query.OrderByDescending(x => x.Name),
                Domain.Enums.CategorySortBy.CreatedDate => request.Sort == Domain.Enums.SortOrder.Asc
                    ? query.OrderBy(x => x.CreatedDate)
                    : query.OrderByDescending(x => x.CreatedDate),
                _ => query.OrderBy(x => x.Name)
            };

            var totalCount = await query.CountAsync(cancellationToken);

            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<CategoryResult>
            {
                Data = _mapper.Map<List<CategoryResult>>(categories),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

