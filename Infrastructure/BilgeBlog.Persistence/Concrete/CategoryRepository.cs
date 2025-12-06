using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;

namespace BilgeBlog.Persistence.Concrete
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BilgeBlogDbContext context) : base(context)
        {
        }
    }
}

