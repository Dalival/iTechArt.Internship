using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArt.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;

        private readonly Dictionary<Type, object> _repositories;
        private readonly Dictionary<Type, object> _customRepositories;

        private bool _disposed;


        public UnitOfWork(TContext context)
        {
            _context = context;

            _repositories = new Dictionary<Type, object>();
            _customRepositories = new Dictionary<Type, object>();
        }


        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            var type = typeof(TEntity);

            if (_customRepositories.ContainsKey(type))
            {
                return (IRepository<TEntity>) _customRepositories[type];
            }
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>) _repositories[type];
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


        protected void RegisterRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : IRepository<TEntity>
        {
            var entityType = typeof(TEntity);
            var repositoryType = typeof(TRepository);
            _customRepositories[entityType] = Activator.CreateInstance(repositoryType, _context);
        }


        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositories.Clear();
                    _customRepositories.Clear();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
