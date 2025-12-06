using AutoMapper;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.Application.DTOs.UserDtos.Results;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BilgeBlog.Application.Handlers.UserHandlers.Modify
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<LoginResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null)
                throw new UnauthorizedException("Email veya şifre hatalı.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new UnauthorizedException("Email veya şifre hatalı.");

            var userResult = _mapper.Map<UserResult>(user);
            return new LoginResult { User = userResult };
        }
    }
}

