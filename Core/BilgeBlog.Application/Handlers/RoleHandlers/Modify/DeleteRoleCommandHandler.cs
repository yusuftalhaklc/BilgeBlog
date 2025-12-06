using BilgeBlog.Application.DTOs.RoleDtos.Commands;
using BilgeBlog.Contract.Abstract;
using MediatR;

namespace BilgeBlog.Application.Handlers.RoleHandlers.Modify
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleRepository.RemoveAsync(request.Id);
        }
    }
}

