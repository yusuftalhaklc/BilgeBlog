using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using BilgeBlog.Application.DTOs.TagDtos.Results;
using BilgeBlog.WebApi.Extensions;
using BilgeBlog.WebApi.Requests.PostRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Author")]
        public async Task<ActionResult<BaseResponse<Guid>>> Create([FromBody] CreatePostRequest request)
        {
            var command = _mapper.Map<CreatePostCommand>(request);
            command.UserId = User.GetUserId();
            var postId = await _mediator.Send(command);
            return Ok(BaseResponse<Guid>.Ok(postId, "Blog oluşturma başarılı"));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PagedResult<PostResult>>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllPostsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = User.Identity?.IsAuthenticated == true ? User.GetUserId() : null
            };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<PagedResult<PostResult>>.Ok(result, "Bloglar başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<PostResult>>> GetById(Guid id)
        {
            var query = new GetPostByIdQuery 
            { 
                Id = id,
                UserId = User.Identity?.IsAuthenticated == true ? User.GetUserId() : null
            };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<PostResult>.Ok(result, "Blog başarıyla getirildi"));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<bool>>> Update(Guid id, [FromBody] UpdatePostRequest request)
        {
            var command = _mapper.Map<UpdatePostCommand>(request);
            command.Id = id;
            command.UserId = User.GetUserId();
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Blog güncelleme başarılı"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var command = new DeletePostCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Blog silme başarılı"));
        }

        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<BaseResponse<List<CommentResult>>>> GetComments(Guid postId)
        {
            var query = new GetPostCommentsQuery { PostId = postId };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<List<CommentResult>>.Ok(result, "Yorumlar başarıyla getirildi"));
        }

        [HttpGet("{postId}/likes")]
        public async Task<ActionResult<BaseResponse<List<PostLikeResult>>>> GetLikes(Guid postId)
        {
            var query = new GetPostLikesQuery { PostId = postId };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<List<PostLikeResult>>.Ok(result, "Beğeniler başarıyla getirildi"));
        }

        [HttpGet("{postId}/tags")]
        public async Task<ActionResult<BaseResponse<List<TagResult>>>> GetTags(Guid postId)
        {
            var query = new GetPostTagsQuery { PostId = postId };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<List<TagResult>>.Ok(result, "Taglar başarıyla getirildi"));
        }
    }
}
