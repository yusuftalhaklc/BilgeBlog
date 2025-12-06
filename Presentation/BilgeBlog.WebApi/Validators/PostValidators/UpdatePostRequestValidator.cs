using BilgeBlog.WebApi.Requests.PostRequests;
using FluentValidation;

namespace BilgeBlog.WebApi.Validators.PostValidators
{
    public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.")
                .MinimumLength(50).WithMessage("İçerik en az 50 karakter olmalıdır.");

            RuleForEach(x => x.Tags)
                .NotEmpty().WithMessage("Tag boş olamaz.")
                .MaximumLength(50).WithMessage("Tag en fazla 50 karakter olabilir.")
                .When(x => x.Tags != null && x.Tags.Any());
        }
    }
}

