using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class PostLikeRepository : BaseRepository<PostLike>, IPostLikeRepository
    {
        public PostLikeRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

