using BilgeBlog.Application.DTOs.CommentDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.CommentHandlers.Modify
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public DeleteCommentCommandHandler(
            ICommentRepository commentRepository,
            IUserRepository userRepository,
            IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetAll(false)
                .Include(x => x.Post)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (comment == null)
                throw new NotFoundException("Comment", request.Id);

            var currentUser = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (currentUser == null)
                throw new UnauthorizedException("Kullanıcı bulunamadı.");

            var isAdmin = currentUser.Role?.Name == RoleEnum.Admin.ToString();
            var isCommentOwner = comment.UserId == request.CurrentUserId;
            var isPostOwner = comment.Post?.UserId == request.CurrentUserId;
            var isAuthor = currentUser.Role?.Name == RoleEnum.Author.ToString();

            if (!isAdmin && !isCommentOwner && !(isPostOwner && isAuthor))
                throw new ForbiddenException("Bu yorumu silmek için yetkiniz yok. Sadece yorum sahibi, post sahibi (Author) veya admin bu işlemi yapabilir.");

            return await _commentRepository.RemoveAsync(request.Id);
        }
    }
}

