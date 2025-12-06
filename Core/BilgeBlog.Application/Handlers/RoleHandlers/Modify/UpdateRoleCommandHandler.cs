using BilgeBlog.Application.DTOs.RoleDtos.Commands;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.RoleHandlers.Modify
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (currentUser == null || currentUser.Role?.Name != RoleEnum.Admin.ToString())
                throw new ForbiddenException("Bu işlem için admin yetkisi gereklidir.");

            var role = await _roleRepository.GetByIdAsync(request.Id);
            if (role == null)
                throw new NotFoundException("Role", request.Id);

            role.Name = request.Name;
            return await _roleRepository.Update(role);
        }
    }
}

