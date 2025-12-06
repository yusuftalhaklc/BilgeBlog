using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class PostTagRepository : BaseRepository<PostTag>, IPostTagRepository
    {
        public PostTagRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

