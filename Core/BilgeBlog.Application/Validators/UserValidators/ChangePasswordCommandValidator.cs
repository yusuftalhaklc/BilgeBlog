using BilgeBlog.Application.DTOs.UserDtos.Commands;
using FluentValidation;

namespace BilgeBlog.Application.Validators.UserValidators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

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

