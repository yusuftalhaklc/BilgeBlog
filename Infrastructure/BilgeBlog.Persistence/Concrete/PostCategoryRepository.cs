using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class PostCategoryRepository : BaseRepository<PostCategory>, IPostCategoryRepository
    {
        public PostCategoryRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

