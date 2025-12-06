using AutoMapper;
using BilgeBlog.Application.DTOs.CategoryDtos.Queries;
using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;

namespace BilgeBlog.Application.Handlers.CategoryHandlers.Read
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id, false);
            if (category == null)
                throw new NotFoundException("Category", request.Id);

            return _mapper.Map<CategoryResult>(category);
        }
    }
}

