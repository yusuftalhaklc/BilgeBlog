using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.RoleDtos.Commands;
using BilgeBlog.Application.DTOs.RoleDtos.Queries;
using BilgeBlog.Application.DTOs.RoleDtos.Results;
using BilgeBlog.WebApi.Extensions;
using BilgeBlog.WebApi.Requests.RoleRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<PagedResult<RoleResult>>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new GetAllRolesQuery
            {
                CurrentUserId = User.GetUserId(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<PagedResult<RoleResult>>.Ok(result, "Roller başarıyla getirildi"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<bool>>> Update(Guid id, [FromBody] UpdateRoleRequest request)
        {
            var command = _mapper.Map<UpdateRoleCommand>(request);
            command.Id = id;
            command.CurrentUserId = User.GetUserId();
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Rol güncelleme başarılı"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var command = new DeleteRoleCommand
            {
                Id = id,
                CurrentUserId = User.GetUserId()
            };
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Rol silme başarılı"));
        }
    }
}

