using AutoMapper;
using BilgeBlog.Application.DTOs.PostDtos.Commands;
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
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
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
            return post.Id;
        }
    }
}

