using AutoMapper;
using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Application.Helpers;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Modify
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IPostTagRepository _postTagRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(
            IPostRepository postRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IPostCategoryRepository postCategoryRepository,
            IPostTagRepository postTagRepository,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _postCategoryRepository = postCategoryRepository;
            _postTagRepository = postTagRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request);
            
            // Slug'ı title'dan otomatik oluştur ve unique yap
            post.Slug = await SlugHelper.GenerateUniqueSlugAsync(
                request.Title,
                async (slug) => await _postRepository.GetAll(false)
                    .AnyAsync(p => p.Slug == slug, cancellationToken)
            );

            await _postRepository.AddAsync(post);

            // CategoryId varsa PostCategory oluştur
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

            // Tags varsa işle
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

                    // PostTag oluştur (duplicate kontrolü)
                    var existingPostTag = await _postTagRepository.GetAll(false)
                        .AnyAsync(pt => pt.PostId == post.Id && pt.TagId == tagId, cancellationToken);

                    if (!existingPostTag)
                    {
                        var postTag = new PostTag
                        {
                            PostId = post.Id,
                            TagId = tagId
                        };
                        await _postTagRepository.AddAsync(postTag);
                    }
                }
            }

            return post.Id;
        }
    }
}

