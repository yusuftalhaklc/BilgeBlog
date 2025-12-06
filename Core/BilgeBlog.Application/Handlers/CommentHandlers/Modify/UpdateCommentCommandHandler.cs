using BilgeBlog.Application.DTOs.CommentDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;

namespace BilgeBlog.Application.Handlers.CommentHandlers.Modify
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, bool>
    {
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(request.Id);
            if (comment == null)
                throw new NotFoundException("Comment", request.Id);

            if (comment.UserId != request.CurrentUserId)
                throw new ForbiddenException("Bu yorumu güncellemek için yetkiniz yok. Sadece yorum sahibi bu işlemi yapabilir.");

            comment.Message = request.Message;

            return await _commentRepository.Update(comment);
        }
    }
}

