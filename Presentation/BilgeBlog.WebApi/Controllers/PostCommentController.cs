using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.CommentDtos.Commands;
using BilgeBlog.WebApi.Extensions;
using BilgeBlog.WebApi.Requests.CommentRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/post/{postId}/comment")]
    [Authorize]
    public class PostCommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostCommentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Guid>>> AddComment(Guid postId, [FromBody] CreateCommentRequest request)
        {
            var userId = User.GetUserId();
            var command = _mapper.Map<CreateCommentCommand>(request);
            command.PostId = postId;
            command.UserId = userId;
            command.ParentCommentId = null;
            var commentId = await _mediator.Send(command);
            return Ok(BaseResponse<Guid>.Ok(commentId, "Yorum eklendi"));
        }

        [HttpPost("{commentId}/reply")]
        public async Task<ActionResult<BaseResponse<Guid>>> ReplyToComment(Guid postId, Guid commentId, [FromBody] CreateCommentRequest request)
        {
            var userId = User.GetUserId();
            var command = _mapper.Map<CreateCommentCommand>(request);
            command.PostId = postId;
            command.UserId = userId;
            command.ParentCommentId = commentId;
            var replyCommentId = await _mediator.Send(command);
            return Ok(BaseResponse<Guid>.Ok(replyCommentId, "Yanıt eklendi"));
        }

        [HttpPut("{commentId}")]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateComment(Guid postId, Guid commentId, [FromBody] UpdateCommentRequest request)
        {
            var command = _mapper.Map<UpdateCommentCommand>(request);
            command.Id = commentId;
            command.CurrentUserId = User.GetUserId();
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Yorum güncelleme başarılı"));
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult<BaseResponse<bool>>> DeleteComment(Guid postId, Guid commentId)
        {
            var command = new DeleteCommentCommand
            {
                Id = commentId,
                CurrentUserId = User.GetUserId()
            };
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Yorum silme başarılı"));
        }
    }
}

