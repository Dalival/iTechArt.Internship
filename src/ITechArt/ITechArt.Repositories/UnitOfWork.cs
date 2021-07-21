using System;
using System.Collections.Generic;
using System.Linq;
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
        private bool _disposed;


        public UnitOfWork(TContext context)
        {
            _context = context;

            _repositories = new Dictionary<Type, object>();

            RegisterCustomRepositories();
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>) _repositories[type];
        }

        public TRepository GetCustomRepository<TEntity, TRepository>()
            where TEntity : class
            where TRepository : Repository<TEntity>
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = Activator.CreateInstance(typeof(TRepository), _context);
            }

            return (TRepository) _repositories[type];
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


        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _repositories?.Clear();
                _context.Dispose();
            }

            _disposed = true;
        }

        private void RegisterCustomRepositories()
        {
            var allTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(type => type.GetTypes())
                .ToList();

            var repositoryTypes = allTypes
                .Where(type =>
                    type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(Repository<>))
                .ToList();

            var entityTypes = repositoryTypes
                .Select(type => type.BaseType?.GenericTypeArguments.Single())
                .ToList();

            for (var i = 0; i < repositoryTypes.Count; i++)
            {
                var type = entityTypes[i];
                var repository = Activator.CreateInstance(repositoryTypes[i], _context);

                _repositories[type] = repository;
            }
        }
    }
}
