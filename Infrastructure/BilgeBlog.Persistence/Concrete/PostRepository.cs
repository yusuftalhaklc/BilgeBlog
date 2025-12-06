using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

