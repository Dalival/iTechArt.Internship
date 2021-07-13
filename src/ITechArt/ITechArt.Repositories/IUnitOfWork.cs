using System;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
        
        void Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        TContext Context { get; }
    }
}
