using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Contract.Abstract;
using MediatR;

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
                return false;

            post.Title = request.Title;
            post.Slug = request.Slug;
            post.Content = request.Content;
            post.IsPublished = request.IsPublished;
            post.UserId = request.UserId;

            return await _postRepository.Update(post);
        }
    }
}

