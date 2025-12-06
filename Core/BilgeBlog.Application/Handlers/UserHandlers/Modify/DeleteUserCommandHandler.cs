using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.UserHandlers.Modify
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (currentUser == null || currentUser.Role?.Name != RoleEnum.Admin.ToString())
                throw new ForbiddenException("Bu işlem için admin yetkisi gereklidir.");

            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
                throw new NotFoundException("User", request.Id);

            return await _userRepository.RemoveAsync(request.Id);
        }
    }
}

