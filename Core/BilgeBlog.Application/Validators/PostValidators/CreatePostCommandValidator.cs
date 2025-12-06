using BilgeBlog.Application.DTOs.PostDtos.Commands;
using FluentValidation;

namespace BilgeBlog.Application.Validators.PostValidators
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.")
                .MinimumLength(50).WithMessage("İçerik en az 50 karakter olmalıdır.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

            RuleFor(x => x.Tags)
                .NotNull().WithMessage("Tag listesi boş olamaz.")
                .Must(tags => tags != null && tags.Count == 5).WithMessage("Tam olarak 5 tag girilmelidir.");

            RuleForEach(x => x.Tags)
                .NotEmpty().WithMessage("Tag boş olamaz.")
                .Length(50).WithMessage("Her tag tam olarak 50 karakter olmalıdır.")
                .When(x => x.Tags != null && x.Tags.Count == 5);
        }
    }
}

