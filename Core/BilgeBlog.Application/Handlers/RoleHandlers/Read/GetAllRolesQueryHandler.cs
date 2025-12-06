using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.RoleDtos.Queries;
using BilgeBlog.Application.DTOs.RoleDtos.Results;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.RoleHandlers.Read
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, PagedResult<RoleResult>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<RoleResult>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (currentUser == null || currentUser.Role?.Name != RoleEnum.Admin.ToString())
                throw new ForbiddenException("Bu işlem için admin yetkisi gereklidir.");

            var query = _roleRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var roles = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<RoleResult>
            {
                Data = _mapper.Map<List<RoleResult>>(roles),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

