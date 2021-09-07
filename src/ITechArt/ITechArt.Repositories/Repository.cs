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
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _dbSet;


        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = _dbContext.Set<T>();
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
            var target = await _dbContext.FindAsync<T>(id);

            return target;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var queryWithIncludes = GetQueryWithIncludes(includes);
            var target = await queryWithIncludes.ToListAsync();

            return target;
        }

        public async Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            var queryWithIncludes = GetQueryWithIncludes(includes);
            var target = await queryWithIncludes.Where(predicate).ToListAsync();

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
