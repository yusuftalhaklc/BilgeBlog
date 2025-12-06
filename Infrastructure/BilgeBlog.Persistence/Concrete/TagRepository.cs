using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

