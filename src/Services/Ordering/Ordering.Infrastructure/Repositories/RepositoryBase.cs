using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System.Linq;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IAsyncRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly OrderDbContext _dbContext;

        public RepositoryBase(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>()
                                    .Where(predicate)
                                    .ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true
            )
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if(predicate  != null)
            {
                query = query.Where(predicate);
            }
            if(!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
            List<Expression<Func<TEntity,
            object>>>? includes = null,
            bool disableTracking = true
            )
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
           
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include)=> current.Include(include));
            }
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
               return await  orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
