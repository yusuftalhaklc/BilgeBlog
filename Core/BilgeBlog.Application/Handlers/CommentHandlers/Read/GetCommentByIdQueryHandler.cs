using AutoMapper;
using BilgeBlog.Application.DTOs.CommentDtos.Queries;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using BilgeBlog.Contract.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BilgeBlog.Application.Handlers.CommentHandlers.Read
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentResult>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentResult> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetAll(false)
                .Include(x => x.Post)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (comment == null)
                return null!;

            return _mapper.Map<CommentResult>(comment);
        }
    }
}

