using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Application.Helpers;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Modify
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);
            if (post == null)
                throw new NotFoundException("Post", request.Id);

            // Title değiştiyse veya slug yoksa, slug'ı yeniden oluştur
            var titleChanged = post.Title != request.Title;
            post.Title = request.Title;
            
            if (titleChanged || string.IsNullOrEmpty(post.Slug))
            {
                post.Slug = await SlugHelper.GenerateUniqueSlugAsync(
                    request.Title,
                    async (slug) => await _postRepository.GetAll(false)
                        .AnyAsync(p => p.Slug == slug && p.Id != request.Id, cancellationToken)
                );
            }
            
            post.Content = request.Content;
            post.IsPublished = request.IsPublished;
            post.UserId = request.UserId;

            return await _postRepository.Update(post);
        }
    }
}

