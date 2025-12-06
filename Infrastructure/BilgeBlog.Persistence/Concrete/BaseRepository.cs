using BilgeBlog.Contract.Abstract;
using BilgeBlog.Domain.Entities;
using BilgeBlog.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BilgeBlog.Persistence.Concrete
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly BilgeBlogDbContext _context;
        protected readonly DbSet<T> _table;

        public BaseRepository(BilgeBlogDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = _table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            var query = _table.Where(expression);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            var query = _table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression) ?? null!;
        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
            var query = _table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.Id == id) ?? null!;
        }

        public async Task<bool> AddAsync(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
            }
            await _table.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Remove(T entity)
        {
            entity.DeletedDate = DateTime.UtcNow;
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;
            entity.DeletedDate = DateTime.UtcNow;
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletedDate = DateTime.UtcNow;
            }
            _table.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

