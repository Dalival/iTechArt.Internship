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
            if (includes == null || includes.Length == 0)
            {
                var entities = await _dbSet.ToListAsync();

                return entities;
            }

            var queryWithIncludes = GetQueryWithIncludes(includes);
            var entitiesWithIncludes = await queryWithIncludes.ToListAsync();

            return entitiesWithIncludes;
        }

        public async Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                var target = await _dbSet.Where(predicate).ToListAsync();

                return target;
            }

            var queryWithIncludes = GetQueryWithIncludes(includes);
            var entitiesWithIncludes = await queryWithIncludes.Where(predicate).ToListAsync();

            return entitiesWithIncludes;
        }

        public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var target = await _dbSet.SingleOrDefaultAsync(predicate);

            return target;
        }

        public async Task<IReadOnlyCollection<T>> GetPaginatedAsync(int fromPosition, int amount,
            params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                var target = await _dbSet
                    .Skip(fromPosition)
                    .Take(amount)
                    .ToListAsync();

                return target;
            }

            var queryWithIncludes = GetQueryWithIncludes(includes);
            var targetWithIncludes = await queryWithIncludes
                .Skip(fromPosition)
                .Take(amount)
                .ToListAsync();

            return targetWithIncludes;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            var target = await _dbSet.AnyAsync(predicate);

            return target;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var recordsAmount = predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);

            return recordsAmount;
        }


        private IQueryable<T> GetQueryWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            var query = includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                (_dbSet, (current, expression) => current.Include(expression));

            return query;
        }
    }
}
