using System;

namespace ITechArt.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
         IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IDbModel;

        void Commit();
    }
}
