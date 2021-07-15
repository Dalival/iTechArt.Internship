using System;
using System.Collections.Generic;
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

        public void Commit()
        {
            Context.SaveChanges();
        }


        internal object GetOrAddRepository(Type type, object repository)
        {
            _repositories ??= new Dictionary<Type, object>();

            if (_repositories.TryGetValue(type, out var existedRepository))
            {
                return existedRepository;
            }

            _repositories.Add(type, repository);
            return repository;
        }
    }
}
