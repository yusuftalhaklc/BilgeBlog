using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Modify
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public DeletePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);
            if (post == null)
                throw new NotFoundException("Post", request.Id);

            var currentUser = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (currentUser == null)
                throw new UnauthorizedException("Kullanıcı bulunamadı.");

            var isAdmin = currentUser.Role?.Name == RoleEnum.Admin.ToString();
            var isPostOwner = post.UserId == request.CurrentUserId;

            if (!isAdmin && !isPostOwner)
                throw new ForbiddenException("Bu postu silmek için yetkiniz yok. Sadece post sahibi veya admin bu işlemi yapabilir.");

            return await _postRepository.RemoveAsync(request.Id);
        }
    }
}

