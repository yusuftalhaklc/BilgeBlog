using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.CommentDtos.Queries;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.CommentHandlers.Read
{
    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, PagedResult<CommentResult>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetAllCommentsQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CommentResult>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = _commentRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.User);

            var totalCount = await query.CountAsync(cancellationToken);

            var comments = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<CommentResult>
            {
                Data = _mapper.Map<List<CommentResult>>(comments),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

