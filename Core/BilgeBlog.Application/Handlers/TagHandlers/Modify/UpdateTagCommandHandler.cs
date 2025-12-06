using BilgeBlog.Application.DTOs.TagDtos.Commands;
using BilgeBlog.Contract.Abstract;
using MediatR;

namespace BilgeBlog.Application.Handlers.TagHandlers.Modify
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, bool>
    {
        private readonly ITagRepository _tagRepository;

        public UpdateTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(request.Id);
            if (tag == null)
                return false;

            tag.Name = request.Name;
            return await _tagRepository.Update(tag);
        }
    }
}

