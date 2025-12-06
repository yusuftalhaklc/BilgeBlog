using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.RoleDtos.Queries;
using BilgeBlog.Application.DTOs.RoleDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.RoleHandlers.Read
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, PagedResult<RoleResult>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<RoleResult>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
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

