using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private Dictionary<(Type type, string name), object> _repositories;


        public TContext Context { get; }


        public UnitOfWork(TContext context)
        {
            Context = context;
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

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }


        internal object GetOrAddRepository(Type type, object repository)
        {
            _repositories ??= new Dictionary<(Type type, string name), object>();

            if (_repositories.TryGetValue((type, repository.GetType().FullName),
                out var newRepository))
            {
                return newRepository;
            }

            _repositories.Add((type, repository.GetType().FullName), repository);
            return repository;
        }
    }
}
