using BilgeBlog.Application.DTOs.PostCategoryDtos.Commands;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostCategoryHandlers.Modify
{
    public class DeletePostCategoryCommandHandler : IRequestHandler<DeletePostCategoryCommand, bool>
    {
        private readonly IPostCategoryRepository _postCategoryRepository;

        public DeletePostCategoryCommandHandler(IPostCategoryRepository postCategoryRepository)
        {
            _postCategoryRepository = postCategoryRepository;
        }

        public async Task<bool> Handle(DeletePostCategoryCommand request, CancellationToken cancellationToken)
        {
            var postCategory = await _postCategoryRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.PostId == request.PostId && x.CategoryId == request.CategoryId, cancellationToken);

            if (postCategory == null)
                return false;

            return await _postCategoryRepository.Remove(postCategory);
        }
    }
}

