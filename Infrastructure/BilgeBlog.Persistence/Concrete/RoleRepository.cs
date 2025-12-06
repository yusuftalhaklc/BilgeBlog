using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

