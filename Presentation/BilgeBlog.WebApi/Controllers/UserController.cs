using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.Application.DTOs.UserDtos.Results;
using BilgeBlog.WebApi.Extensions;
using BilgeBlog.WebApi.Requests.UserRequests;
using BilgeBlog.WebApi.Services;
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
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, ITokenService tokenService, IMapper mapper)
        {
            _mediator = mediator;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<LoginResult>>> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            result.Token = _tokenService.GenerateToken(result.User);
            return Ok(BaseResponse<LoginResult>.Ok(result, "Giriş başarılı"));
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse<Guid>>> Register([FromBody] RegisterUserCommand command)
        {
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
    }
}

