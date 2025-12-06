using AutoMapper;
using BilgeBlog.Application.DTOs.CategoryDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.CategoryHandlers.Modify
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Aynı isimde kategori var mı kontrol et
            var existingCategory = await _categoryRepository.GetAll(false)
                .FirstOrDefaultAsync(c => c.Name.ToLower() == request.Name.Trim().ToLower(), cancellationToken);

            if (existingCategory != null)
                throw new ConflictException($"'{request.Name}' isimli kategori zaten mevcut.");

            var category = _mapper.Map<Category>(request);
            category.Name = request.Name.Trim();
            await _categoryRepository.AddAsync(category);
            return category.Id;
        }
    }
}

