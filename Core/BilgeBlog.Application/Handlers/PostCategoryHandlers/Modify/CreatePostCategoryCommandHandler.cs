using BilgeBlog.Application.DTOs.PostCategoryDtos.Commands;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostCategoryHandlers.Modify
{
    public class CreatePostCategoryCommandHandler : IRequestHandler<CreatePostCategoryCommand, bool>
    {
        private readonly IPostCategoryRepository _postCategoryRepository;

        public CreatePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository)
        {
            _postCategoryRepository = postCategoryRepository;
        }

        public async Task<bool> Handle(CreatePostCategoryCommand request, CancellationToken cancellationToken)
        {
            var existing = await _postCategoryRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.CategoryId == request.CategoryId, cancellationToken);

            if (existing != null)
                return false;

            var postCategory = new PostCategory
            {
                PostId = request.PostId,
                CategoryId = request.CategoryId
            };

            return await _postCategoryRepository.AddAsync(postCategory);
        }
    }
}

