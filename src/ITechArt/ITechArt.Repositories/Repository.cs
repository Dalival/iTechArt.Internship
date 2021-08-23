using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace ITechArt.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        private DbSet<T> _dbSet;


        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = _dbContext.Set<T>();
        }


        public async Task<T> GetByIdAsync(params object[] id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetWithIncludesAsync<TProperty>(params Expression<Func<T, TProperty>>[] navigationProperties)
        {
            var query = _dbSet.AsQueryable();

            foreach (var property in navigationProperties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
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
    }
}
