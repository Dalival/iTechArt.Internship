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
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var queryWithIncludes = GetQueryWithIncludes(includes);
            var entitiesWithIncludes = await queryWithIncludes.ToListAsync();

            return entitiesWithIncludes;
        }

        public async Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            var queryWithIncludes = GetQueryWithIncludes(includes);
            var entitiesWithIncludes = await queryWithIncludes.Where(predicate).ToListAsync();

            return entitiesWithIncludes;
        }

        public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(predicate);

            return entity;
        }

        public virtual async Task<IReadOnlyCollection<T>> GetPaginatedAsync(
            int skip,
            int take,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var entities = await GetPaginatedQuery(_dbSet, skip, take, orderStrategies).ToListAsync();

            return entities;
        }

        public virtual async Task<IReadOnlyCollection<T>> GetWherePaginatedAsync(
            int skip,
            int take,
            Expression<Func<T, bool>> predicate,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var targetQuery = GetWherePaginatedQuery(_dbSet, skip, take, predicate, orderStrategies);
            var entities = await targetQuery.ToListAsync();

            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            var isEntityExist = await _dbSet.AnyAsync(predicate);

            return isEntityExist;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var count = predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);

            return count;
        }


        protected IQueryable<T> GetPaginatedQuery(
            IQueryable<T> query,
            int skip,
            int take,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var queryToPaginate = query;

            if (orderStrategies.Length != 0)
            {
                var firstStrategy = orderStrategies.First();
                var orderedQuery = firstStrategy.Ascending
                    ? query.OrderBy(firstStrategy.OrderBy)
                    : query.OrderByDescending(firstStrategy.OrderBy);

                orderedQuery = orderStrategies
                    .Skip(1)
                    .Aggregate(orderedQuery, (current, strategy) => strategy.Ascending
                        ? current.ThenBy(strategy.OrderBy)
                        : current.ThenByDescending(strategy.OrderBy));

                queryToPaginate = orderedQuery;
            }

            var paginatedQuery = queryToPaginate.Skip(skip).Take(take);

            return paginatedQuery;
        }

        protected IQueryable<T> GetWherePaginatedQuery(
            IQueryable<T> query,
            int skip,
            int take,
            Expression<Func<T, bool>> predicate,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var filterQuery = query.Where(predicate);
            var paginatedQuery = GetPaginatedQuery(filterQuery, skip, take, orderStrategies);

            return paginatedQuery;
        }


        private IQueryable<T> GetQueryWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            if (includes.Length == 0)
            {
                return _dbSet;
            }

            var queryWithIncludes = includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(
                _dbSet,
                (current, include) => current.Include(include));

            return queryWithIncludes;
        }
    }
}
