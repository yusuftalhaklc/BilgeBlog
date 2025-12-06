using AutoMapper;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using BilgeBlog.Application.DTOs.PostDtos.Queries;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.PostHandlers.Read
{
    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, List<CommentResult>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetPostCommentsQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentResult>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.User)
                .Where(x => x.PostId == request.PostId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<CommentResult>>(comments);
        }
    }
}

