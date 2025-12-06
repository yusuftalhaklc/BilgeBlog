using AutoMapper;
using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, PagedResult<CommentListItemResult>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetPostCommentsQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CommentListItemResult>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = _commentRepository.GetAll(false)
                .Include(x => x.User)
                .Where(x => x.PostId == request.PostId);

            var totalCount = await query.CountAsync(cancellationToken);

            var comments = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<CommentListItemResult>
            {
                Data = _mapper.Map<List<CommentListItemResult>>(comments),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

