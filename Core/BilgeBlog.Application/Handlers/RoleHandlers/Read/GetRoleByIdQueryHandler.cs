using AutoMapper;
using BilgeBlog.Application.DTOs.RoleDtos.Queries;
using BilgeBlog.Application.DTOs.RoleDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;

namespace BilgeBlog.Application.Handlers.RoleHandlers.Read
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleResult>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleResult> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id, false);
            if (role == null)
                return null!;

            return _mapper.Map<RoleResult>(role);
        }
    }
}

