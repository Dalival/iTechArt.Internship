using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly Dictionary<Type, object> _repositories;


        private TContext Context { get; }


        public UnitOfWork(TContext context)
        {
            Context = context;
            _repositories = new Dictionary<Type, object>();
        }


        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (_repositories.TryGetValue(typeof(TEntity), out var existedRepository))
            {
                return (IRepository<TEntity>) existedRepository;
            }

            var newRepository = new Repository<TEntity>(Context);
            _repositories.Add(typeof(TEntity), newRepository);

            return newRepository;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
