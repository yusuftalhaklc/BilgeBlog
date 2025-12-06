using BilgeBlog.Application.DTOs.CategoryDtos.Commands;
using FluentValidation;

namespace BilgeBlog.Application.Validators.CategoryValidators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.");
        }
    }
}

