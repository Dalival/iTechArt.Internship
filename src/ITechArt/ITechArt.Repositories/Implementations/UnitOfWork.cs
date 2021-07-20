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
            var customRepository = Context.GetService<IRepository<TEntity>>();
            if (customRepository != null)
            {
                return customRepository;
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(Context);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
