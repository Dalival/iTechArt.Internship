using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ITechArt.Repositories.Implementations
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;

        private readonly Dictionary<Type, object> _repositories;
        private bool _disposed;


        public UnitOfWork(TContext context)
        {
            _context = context;

            _repositories = new Dictionary<Type, object>();
        }


        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            var customRepository = _context.GetService<IRepository<TEntity>>();
            if (customRepository != null)
            {
                return customRepository;
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _repositories?.Clear();
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
