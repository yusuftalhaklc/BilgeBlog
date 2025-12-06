using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.PostLikeDtos.Commands;
using BilgeBlog.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/post/{postId}/like")]
    [Authorize]
    public class PostLikeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostLikeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<bool>>> LikePost(Guid postId)
        {
            var userId = User.GetUserId();
            var command = new CreatePostLikeCommand
            {
                PostId = postId,
                UserId = userId
            };
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Post beğenildi"));
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResponse<bool>>> UnlikePost(Guid postId)
        {
            var userId = User.GetUserId();
            var command = new DeletePostLikeCommand
            {
                PostId = postId,
                UserId = userId
            };
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Beğeni kaldırıldı"));
        }
    }
}

