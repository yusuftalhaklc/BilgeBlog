using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

