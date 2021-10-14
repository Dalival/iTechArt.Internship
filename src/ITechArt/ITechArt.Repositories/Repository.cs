using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Common;
using ITechArt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public virtual async Task<IReadOnlyCollection<T>> GetPaginatedAsync(
            int skip,
            int take,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var targetEntities = await GetPaginatedCore(skip, take, orderStrategies).ToListAsync();

            return targetEntities;
        }

        public virtual async Task<IReadOnlyCollection<T>> GetWherePaginatedAsync(
            int skip,
            int take,
            Expression<Func<T, bool>> predicate,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var query = GetPaginatedCore(skip, take, orderStrategies);
            var filteredEntities = await query.Where(predicate).ToListAsync();

            return filteredEntities;
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


        protected IQueryable<T> GetPaginatedCore(
            int skip,
            int take,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            IOrderedQueryable<T> orderedQuery = null;

            if (orderStrategies != null && orderStrategies.Length != 0)
            {
                var firstStrategy = orderStrategies[0];
                orderedQuery = firstStrategy.Ascending
                    ? _dbSet.OrderBy(firstStrategy.OrderBy)
                    : _dbSet.OrderByDescending(firstStrategy.OrderBy);
                for (var i = 1; i < orderStrategies.Length; i++)
                {
                    var strategy = orderStrategies[i];
                    orderedQuery = strategy.Ascending
                        ? orderedQuery.ThenBy(strategy.OrderBy)
                        : orderedQuery.ThenByDescending(strategy.OrderBy);
                }
            }

            var filteredQuery = orderedQuery == null
                ? _dbSet.Skip(skip).Take(take)
                : orderedQuery.Skip(skip).Take(take);

            return filteredQuery;
        }


        private IQueryable<T> GetQueryWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            var query = includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                (_dbSet, (current, expression) => current.Include(expression));

            return query;
        }
    }
}
