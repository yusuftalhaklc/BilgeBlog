using BilgeBlog.Application.DTOs.UserDtos.Results;
using MediatR;

namespace BilgeBlog.Application.DTOs.UserDtos.Queries
{
    public class GetUserByIdQuery : IRequest<UserResult>
    {
        public Guid Id { get; set; }
    }
}

