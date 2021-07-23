﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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


        public async Task<T> GetByIdAsync(params object[] id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Add(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}