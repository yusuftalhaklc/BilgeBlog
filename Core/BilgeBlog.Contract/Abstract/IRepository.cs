using BilgeBlog.Domain.Entities;
using System.Linq.Expressions;

namespace BilgeBlog.Contract.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<bool> Update(T entity);
        Task<bool> Remove(T entity);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> RemoveRange(List<T> entities);
        Task<int> SaveAsync();
    }
}

