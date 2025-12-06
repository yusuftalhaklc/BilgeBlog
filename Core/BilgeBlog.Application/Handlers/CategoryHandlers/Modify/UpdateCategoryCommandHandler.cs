using BilgeBlog.Application.DTOs.CategoryDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.CategoryHandlers.Modify
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
                throw new NotFoundException("Category", request.Id);

            // Aynı isimde başka bir kategori var mı kontrol et
            var existingCategory = await _categoryRepository.GetAll(false)
                .FirstOrDefaultAsync(c => c.Name.ToLower() == request.Name.Trim().ToLower() && c.Id != request.Id, cancellationToken);

            if (existingCategory != null)
                throw new ConflictException($"'{request.Name}' isimli kategori zaten mevcut.");

            category.Name = request.Name.Trim();
            return await _categoryRepository.Update(category);
        }
    }
}

