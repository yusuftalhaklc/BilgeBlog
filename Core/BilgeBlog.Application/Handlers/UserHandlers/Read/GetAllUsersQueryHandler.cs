using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.UserDtos.Queries;
using BilgeBlog.Application.DTOs.UserDtos.Results;
using BilgeBlog.Application.Exceptions;
using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.UserHandlers.Read
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PagedResult<UserResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UserResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetAll(false)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == request.CurrentUserId, cancellationToken);

            if (currentUser == null || currentUser.Role?.Name != RoleEnum.Admin.ToString())
                throw new ForbiddenException("Bu işlem için admin yetkisi gereklidir.");

            var baseQuery = _userRepository.GetAll(false);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                baseQuery = baseQuery.Where(x => 
                    x.FirstName.ToLower().Contains(searchLower) ||
                    x.LastName.ToLower().Contains(searchLower) ||
                    x.Email.ToLower().Contains(searchLower));
            }

            var query = baseQuery.Include(x => x.Role);

            var totalCount = await query.CountAsync(cancellationToken);

            var users = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var result = new PagedResult<UserResult>
            {
                Data = _mapper.Map<List<UserResult>>(users),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return result;
        }
    }
}

