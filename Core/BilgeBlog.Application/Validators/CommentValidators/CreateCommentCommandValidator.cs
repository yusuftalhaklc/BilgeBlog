using BilgeBlog.Application.DTOs.CommentDtos.Commands;
using FluentValidation;

namespace BilgeBlog.Application.Validators.CommentValidators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Yorum mesajı boş olamaz.")
                .MinimumLength(5).WithMessage("Yorum en az 5 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post ID boş olamaz.");
        }
    }
}

