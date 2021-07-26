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
        private readonly Dictionary<Type, Type> _customRepositoriesTypes;
        private readonly Dictionary<Type, object> _customRepositories;

        private bool _disposed;


        public UnitOfWork(TContext context)
        {
            _context = context;

            _repositories = new Dictionary<Type, object>();
            _customRepositoriesTypes = new Dictionary<Type, Type>();
            _customRepositories = new Dictionary<Type, object>();
        }


        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            var type = typeof(TEntity);

            if (_repositories.TryGetValue(type, out var repository))
            {
                return (IRepository<TEntity>) repository;
            }

            if (_customRepositories.TryGetValue(type, out var customRepository))
            {
                return (IRepository<TEntity>) customRepository;
            }

            if (_customRepositoriesTypes.TryGetValue(type, out var customRepositoryType))
            {
                var newCustomRepository = Activator.CreateInstance(customRepositoryType, _context);
                _customRepositories.Add(type, newCustomRepository);

                return (IRepository<TEntity>) newCustomRepository;
            }

            var newRepository = new Repository<TEntity>(_context);
            _repositories.Add(type, newRepository);

            return newRepository;
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
            _customRepositoriesTypes.Add(entityType, repositoryType);
        }


        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositories.Clear();
                    _customRepositoriesTypes.Clear();
                    _customRepositories.Clear();
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
