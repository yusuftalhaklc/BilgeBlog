using AutoMapper;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.Application.DTOs.UserDtos.Results;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.UserHandlers.Modify
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<LoginResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserIdFromToken(request.Token);

            var user = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
                throw new UnauthorizedException("Geçersiz refresh token.");

            
            var newToken = _tokenService.GenerateAccessToken(user, user.Role);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userRepository.Update(user);

            var userResult = _mapper.Map<UserResult>(user);
            return new LoginResult
            {
                User = userResult,
                Token = newToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}

