using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

