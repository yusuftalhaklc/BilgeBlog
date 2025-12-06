using BilgeBlog.Application.DTOs.CommentDtos.Commands;
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
                return false;

            comment.Message = request.Message;
            comment.PostId = request.PostId;
            comment.UserId = request.UserId;
            comment.ParentCommentId = request.ParentCommentId;

            return await _commentRepository.Update(comment);
        }
    }
}

