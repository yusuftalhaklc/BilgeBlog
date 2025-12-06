using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.Application.DTOs.UserDtos.Results;
using BilgeBlog.WebApi.Extensions;
using BilgeBlog.WebApi.Requests.UserRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<LoginResult>>> Login([FromBody] LoginUserRequest request)
        {
            var command = _mapper.Map<LoginUserCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<LoginResult>.Ok(result, "Giriş başarılı"));
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse<Guid>>> Register([FromBody] RegisterUserRequest request)
        {
            var command = _mapper.Map<RegisterUserCommand>(request);
            var userId = await _mediator.Send(command);
            return Ok(BaseResponse<Guid>.Ok(userId, "Kayıt başarılı"));
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<bool>>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.GetUserId();
            var command = _mapper.Map<ChangePasswordCommand>(request);
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Şifre değiştirme başarılı"));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<BaseResponse<LoginResult>>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var command = _mapper.Map<RefreshTokenCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<LoginResult>.Ok(result, "Token yenileme başarılı"));
        }
    }
}

