using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;
using BCrypt.Net;

namespace BilgeBlog.Application.Handlers.UserHandlers.Modify
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("User", request.UserId);

            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash);
            if (!isOldPasswordValid)
                throw new BadRequestException("Eski şifre hatalı.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            return await _userRepository.Update(user);
        }
    }
}

