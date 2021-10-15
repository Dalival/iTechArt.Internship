﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var queryWithIncludes = GetQueryWithIncludes(_dbSet, includes);
            var entitiesWithIncludes = await queryWithIncludes.ToListAsync();

            return entitiesWithIncludes;
        }

        public async Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            var queryWithIncludes = GetQueryWithIncludes(_dbSet, includes);
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
            var entities = await GetPaginatedCore(_dbSet, skip, take, orderStrategies).ToListAsync();

            return entities;
        }

        public virtual async Task<IReadOnlyCollection<T>> GetWherePaginatedAsync(
            int skip,
            int take,
            Expression<Func<T, bool>> predicate,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var filteredQuery = _dbSet.Where(predicate);
            var targetQuery = GetPaginatedCore(filteredQuery, skip, take, orderStrategies);
            var targetEntities = await targetQuery.ToListAsync();

            return targetEntities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            var isEntityExist = await _dbSet.AnyAsync(predicate);

            return isEntityExist;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var recordsAmount = predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);

            return recordsAmount;
        }


        protected IQueryable<T> GetPaginatedCore(
            IQueryable<T> query,
            int skip,
            int take,
            params EntityOrderStrategy<T>[] orderStrategies)
        {
            var orderedQuery = (IOrderedQueryable<T>) query;

            if (orderStrategies != null && orderStrategies.Length != 0)
            {
                var firstStrategy = orderStrategies[0];
                orderedQuery = firstStrategy.Ascending
                    ? query.OrderBy(firstStrategy.OrderBy)
                    : query.OrderByDescending(firstStrategy.OrderBy);
                for (var i = 1; i < orderStrategies.Length; i++)
                {
                    var strategy = orderStrategies[i];
                    orderedQuery = strategy.Ascending
                        ? orderedQuery.ThenBy(strategy.OrderBy)
                        : orderedQuery.ThenByDescending(strategy.OrderBy);
                }
            }

            var filteredQuery = orderedQuery.Skip(skip).Take(take);

            return filteredQuery;
        }


        private IQueryable<T> GetQueryWithIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                return query;
            }

            var queryWithIncludes = includes.Aggregate(query, (current, include) => current.Include(include));

            return queryWithIncludes;
        }
    }
}
