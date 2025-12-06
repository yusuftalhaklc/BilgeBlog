using BilgeBlog.WebApi.Requests.CategoryRequests;
using FluentValidation;

namespace BilgeBlog.WebApi.Validators.CategoryValidators
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.");
        }
    }
}

