using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private Dictionary<Type, object> _repositories;


        public TContext Context { get; }


        public UnitOfWork(TContext context)
        {
            Context = context;
            _repositories = new Dictionary<Type, object>();
        }


        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IDbModel
        {
            return (IRepository<TEntity>) GetOrAddRepository(typeof(TEntity), 
                new Repository<TEntity>(Context));
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }


        internal object GetOrAddRepository(Type type, object repository)
        {
            if (_repositories.TryGetValue(type, out var existedRepository))
            {
                return existedRepository;
            }

            _repositories.Add(type, repository);
            return repository;
        }
    }
}
