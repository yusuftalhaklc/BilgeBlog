using BilgeBlog.WebApi.Requests.UserRequests;
using FluentValidation;

namespace BilgeBlog.WebApi.Validators.UserValidators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Eski şifre boş olamaz.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Yeni şifre en fazla 100 karakter olabilir.")
                .NotEqual(x => x.OldPassword).WithMessage("Yeni şifre eski şifre ile aynı olamaz.");
        }
    }
}

