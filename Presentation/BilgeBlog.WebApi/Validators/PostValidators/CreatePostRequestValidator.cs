using BilgeBlog.WebApi.Requests.PostRequests;
using FluentValidation;

namespace BilgeBlog.WebApi.Validators.PostValidators
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.")
                .MinimumLength(50).WithMessage("İçerik en az 50 karakter olmalıdır.");

            RuleFor(x => x.Tags)
                .NotNull().WithMessage("Tag listesi boş olamaz.")
                .Must(tags => tags != null && tags.Count == 5).WithMessage("Tam olarak 5 tag girilmelidir.");

            RuleForEach(x => x.Tags)
                .NotEmpty().WithMessage("Tag boş olamaz.")
                .MinimumLength(2).WithMessage("Her tag en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Her tag en fazla 50 karakter olmalıdır.")
                .When(x => x.Tags != null && x.Tags.Count == 5);
        }
    }
}

