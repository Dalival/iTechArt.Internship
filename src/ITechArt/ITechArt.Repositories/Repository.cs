using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ITechArt.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly DbSet<T> _dbSet;


        public Repository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }


        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IReadOnlyCollection<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IReadOnlyCollection<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IReadOnlyCollection<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<T> GetByIdAsync(params object[] id)
        {
            var target = await _dbSet.FindAsync(id);

            return target;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            List<T> target;

            if (includes == null || includes.Length == 0)
            {
                target = await _dbSet.ToListAsync();
            }
            else
            {
                var queryWithIncludes = GetQueryWithIncludes(includes);
                target = await queryWithIncludes.ToListAsync();
            }

            return target;
        }

        public async Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            List<T> target;

            if (includes == null || includes.Length == 0)
            {
                target = await _dbSet.Where(predicate).ToListAsync();
            }
            else
            {
                var queryWithIncludes = GetQueryWithIncludes(includes);
                target = await queryWithIncludes.Where(predicate).ToListAsync();
            }

            return target;
        }

        public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var target = await _dbSet.SingleOrDefaultAsync(predicate);

            return target;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            var target = await _dbSet.AnyAsync(predicate);

            return target;
        }

        public async Task<IReadOnlyCollection<T>> TakeFrom(int startPosition, int amount, params Expression<Func<T, object>>[] includes)
        {
            List<T> target;

            if (includes == null || includes.Length == 0)
            {
                target = await _dbSet
                    .Skip(startPosition)
                    .Take(amount)
                    .ToListAsync();
            }
            else
            {
                var queryWithIncludes = GetQueryWithIncludes(includes);
                target = await queryWithIncludes
                    .Skip(startPosition)
                    .Take(amount)
                    .ToListAsync();
            }

            return target;
        }

        public async Task<int> Count()
        {
            var recordsAmount = await _dbSet.CountAsync();

            return recordsAmount;
        }


        private IIncludableQueryable<T, object> GetQueryWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            var queryWithIncludes = includes
                .Skip(1)
                .Aggregate(_dbSet.Include(includes.First()),
                    (current, include) => current.Include(include));

            return queryWithIncludes;
        }
    }
}
