using AutoMapper;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BilgeBlog.Application.Handlers.UserHandlers.Modify
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (existingUser != null)
                throw new Exception("Bu email adresi zaten kullanılıyor.");

            var authorRole = await _roleRepository.GetAll(false)
                .FirstOrDefaultAsync(x => x.Name == RoleEnum.Author.ToString(), cancellationToken);

            if (authorRole == null)
                throw new Exception("Author rolü bulunamadı. Lütfen veritabanını seed edin.");

            var user = _mapper.Map<User>(request);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.RoleId = authorRole.Id;
            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}

