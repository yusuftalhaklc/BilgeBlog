using AutoMapper;
using BilgeBlog.Application.DTOs.CategoryDtos.Commands;
using BilgeBlog.Application.DTOs.CategoryDtos.Queries;
using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.WebApi.Requests.CategoryRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<Guid>>> Create([FromBody] CreateCategoryRequest request)
        {
            var command = _mapper.Map<CreateCategoryCommand>(request);
            var categoryId = await _mediator.Send(command);
            return Ok(BaseResponse<Guid>.Ok(categoryId, "Kategori oluşturma başarılı"));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PagedResult<CategoryResult>>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var query = new GetAllCategoriesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<PagedResult<CategoryResult>>.Ok(result, "Kategoriler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CategoryResult>>> GetById(Guid id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<CategoryResult>.Ok(result, "Kategori başarıyla getirildi"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<bool>>> Update(Guid id, [FromBody] UpdateCategoryRequest request)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(request);
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Kategori güncelleme başarılı"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Ok(result, "Kategori silme başarılı"));
        }
    }
}

