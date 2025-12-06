using BilgeBlog.WebApi.Requests.CommentRequests;
using FluentValidation;

namespace BilgeBlog.WebApi.Validators.CommentValidators
{
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Yorum mesajı boş olamaz.")
                .MinimumLength(5).WithMessage("Yorum en az 5 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");
        }
    }
}

