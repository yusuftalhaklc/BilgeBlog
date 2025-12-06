using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Application.Helpers;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Modify
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IPostTagRepository _postTagRepository;

        public UpdatePostCommandHandler(
            IPostRepository postRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IPostCategoryRepository postCategoryRepository,
            IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _postCategoryRepository = postCategoryRepository;
            _postTagRepository = postTagRepository;
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

            // Mevcut PostCategory'leri sil
            var existingPostCategories = await _postCategoryRepository.GetAll(false)
                .Where(pc => pc.PostId == post.Id)
                .ToListAsync(cancellationToken);
            
            foreach (var postCategory in existingPostCategories)
            {
                await _postCategoryRepository.Remove(postCategory);
            }

            // Yeni CategoryId varsa PostCategory oluştur
            if (request.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value, false);
                if (category == null)
                    throw new NotFoundException("Category", request.CategoryId.Value);

                var postCategory = new PostCategory
                {
                    PostId = post.Id,
                    CategoryId = request.CategoryId.Value
                };
                await _postCategoryRepository.AddAsync(postCategory);
            }

            // Mevcut PostTag'leri sil
            var existingPostTags = await _postTagRepository.GetAll(false)
                .Where(pt => pt.PostId == post.Id)
                .ToListAsync(cancellationToken);
            
            foreach (var postTag in existingPostTags)
            {
                await _postTagRepository.Remove(postTag);
            }

            // Yeni Tags varsa işle
            if (request.Tags != null && request.Tags.Any())
            {
                foreach (var tagName in request.Tags)
                {
                    if (string.IsNullOrWhiteSpace(tagName))
                        continue;

                    // Tag var mı kontrol et
                    var existingTag = await _tagRepository.GetAll(false)
                        .FirstOrDefaultAsync(t => t.Name.ToLower() == tagName.Trim().ToLower(), cancellationToken);

                    Guid tagId;
                    if (existingTag != null)
                    {
                        tagId = existingTag.Id;
                    }
                    else
                    {
                        // Tag yoksa oluştur
                        var newTag = new Tag
                        {
                            Name = tagName.Trim()
                        };
                        await _tagRepository.AddAsync(newTag);
                        tagId = newTag.Id;
                    }

                    // PostTag oluştur
                    var postTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tagId
                    };
                    await _postTagRepository.AddAsync(postTag);
                }
            }

            return await _postRepository.Update(post);
        }
    }
}

